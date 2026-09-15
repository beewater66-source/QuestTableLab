[CmdletBinding()]
param(
    [string]$ProjectPath = (Split-Path -Parent $PSScriptRoot),
    [string]$TemplateVersion = "0.1.0"
)

$ErrorActionPreference = "Stop"

$templateName = "com.christophdorn.template.quest-mr-foundation"
$displayName = "Quest MR Foundation"
$description = "Meta Quest 3 MR foundation with URP, OpenXR, passthrough, MRUK, semantic labels, controller interaction, and tests."
$editorVersion = "6000.3.24f1"
$stagePath = Join-Path ([IO.Path]::GetTempPath()) ("QuestMRFoundation-" + [guid]::NewGuid().ToString("N"))
$tempRoot = [IO.Path]::GetFullPath([IO.Path]::GetTempPath())
$resolvedStage = [IO.Path]::GetFullPath($stagePath)

if (-not $resolvedStage.StartsWith($tempRoot, [StringComparison]::OrdinalIgnoreCase)) {
    throw "Der temporaere Staging-Pfad liegt nicht unterhalb des Temp-Verzeichnisses: $resolvedStage"
}

function Set-YamlLine {
    param(
        [Parameter(Mandatory)] [string]$Content,
        [Parameter(Mandatory)] [string]$Key,
        [Parameter(Mandatory)] [AllowEmptyString()] [string]$Value,
        [int]$Indent = 2
    )

    $spaces = " " * $Indent
    $pattern = "(?m)^" + [regex]::Escape($spaces + $Key + ":") + ".*$"
    $replacement = $spaces + $Key + ": " + $Value
    if (-not [regex]::IsMatch($Content, $pattern)) {
        throw "Ein erwarteter ProjectSettings-Eintrag wurde nicht gefunden: $Key"
    }
    return [regex]::Replace($Content, $pattern, $replacement, 1)
}

try {
    New-Item -ItemType Directory -Path $resolvedStage | Out-Null

    foreach ($folder in @("Assets", "Packages", "ProjectSettings")) {
        $source = Join-Path $ProjectPath $folder
        if (-not (Test-Path -LiteralPath $source -PathType Container)) {
            throw "Erforderlicher Unity-Projektordner fehlt: $source"
        }
        Copy-Item -LiteralPath $source -Destination $resolvedStage -Recurse
    }

    $settingsPath = Join-Path $resolvedStage "ProjectSettings\ProjectSettings.asset"
    $settings = Get-Content -LiteralPath $settingsPath -Raw
    $settings = Set-YamlLine $settings "companyName" "DefaultCompany"
    $settings = Set-YamlLine $settings "productName" "QuestMRFoundation"
    $settings = Set-YamlLine $settings "Android" "com.DefaultCompany.questmrfoundation" 4
    $settings = Set-YamlLine $settings "overrideDefaultApplicationIdentifier" "0"
    $settings = Set-YamlLine $settings "metroPackageName" "QuestMRFoundation"
    $settings = Set-YamlLine $settings "metroApplicationDescription" "QuestMRFoundation"
    $settings = Set-YamlLine $settings "cloudProjectId" ""
    $settings = Set-YamlLine $settings "projectName" ""
    $settings = Set-YamlLine $settings "organizationId" ""
    $settings = Set-YamlLine $settings "cloudEnabled" "0"
    Set-Content -LiteralPath $settingsPath -Value $settings -NoNewline

    & unity projects verify $resolvedStage --expect-editor $editorVersion --strict
    if ($LASTEXITCODE -ne 0) {
        throw "Die strikte Unity-Projektpruefung ist fehlgeschlagen."
    }

    & unity templates create $resolvedStage `
        --name $templateName `
        --display-name $displayName `
        --description $description `
        --template-version $TemplateVersion `
        --keep-project-settings `
        --overwrite
    if ($LASTEXITCODE -ne 0) {
        throw "Die Unity-Template-Erzeugung ist fehlgeschlagen."
    }
}
finally {
    if (Test-Path -LiteralPath $resolvedStage) {
        $checkedStage = [IO.Path]::GetFullPath($resolvedStage)
        if (-not $checkedStage.StartsWith($tempRoot, [StringComparison]::OrdinalIgnoreCase)) {
            throw "Unsicheres Cleanup-Ziel abgelehnt: $checkedStage"
        }
        Remove-Item -LiteralPath $checkedStage -Recurse -Force
    }
}
