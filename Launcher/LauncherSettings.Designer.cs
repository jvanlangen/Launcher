// Launch.LauncherSettings
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

[CompilerGenerated]
[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
internal sealed class LauncherSettings : ApplicationSettingsBase
{
	private static LauncherSettings defaultInstance = (LauncherSettings)SettingsBase.Synchronized(new LauncherSettings());

	public static LauncherSettings Default => defaultInstance;

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("shortcuts")]
	public string ShortcutsPath => (string)this["ShortcutsPath"];

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("Win")]
	public string HotKeyModifier
	{
		get
		{
			return (string)this["HotKeyModifier"];
		}
		set
		{
			this["HotKeyModifier"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("Oemtilde")]
	public string HotKey
	{
		get
		{
			return (string)this["HotKey"];
		}
		set
		{
			this["HotKey"] = value;
		}
	}
}
