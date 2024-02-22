/*
	Unityプロジェクトランチャー
	-使い方-
	ulaunch.exe プロジェクトのパス

	コンパイル方法
	C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe ulaunch.cs
*/


using System;
using System.IO;
using System.Xml;
using System.Diagnostics;

class Program
{
	static void Main(string[] args){
		// コマンドライン引数からプロジェクト名を取得
		string project_name = args.Length > 0 ? args[0] : "";
		if (string.IsNullOrEmpty(project_name)){
			Console.WriteLine("プロジェクト名をコマンドライン引数に指定してください。");
			return;
			}

		// 設定ファイルを特定、読み込み
		string csprojPath = Path.Combine(project_name, "Assembly-CSharp.csproj");
		if (!File.Exists(csprojPath)){
			Console.WriteLine("指定されたパスにcsprojファイルが見つかりません。");
			return;
			}

		// XMLとしてロード
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.Load(csprojPath);

		// 名前空間マネージャ
		XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);

		// 名前空間が存在する？
		string ns="//";
		if (xmlDoc.DocumentElement.NamespaceURI != ""){
			nsmgr.AddNamespace("ns", xmlDoc.DocumentElement.NamespaceURI);
			ns="//ns:";
			}

		// 実行
 		string HintPath = xmlDoc.SelectSingleNode(ns+"HintPath",nsmgr).InnerText;
		string exePath = HintPath.Substring(0, HintPath.IndexOf("\\editor\\Data", StringComparison.OrdinalIgnoreCase))+ "\\Editor\\Unity.exe";

//		Console.WriteLine(exePath + " -projectPath " + project_name);

		var app = new ProcessStartInfo();
		app.UseShellExecute = true;
		app.FileName = exePath;
		app.Arguments = " -projectPath " + project_name;
		Process.Start(app);

	} // Main()


}
