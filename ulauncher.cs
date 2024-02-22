/*
	Unity�v���W�F�N�g�����`���[
	-�g����-
	ulaunch.exe �v���W�F�N�g�̃p�X

	�R���p�C�����@
	C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe ulaunch.cs
*/


using System;
using System.IO;
using System.Xml;
using System.Diagnostics;

class Program
{
	static void Main(string[] args){
		// �R�}���h���C����������v���W�F�N�g�����擾
		string project_name = args.Length > 0 ? args[0] : "";
		if (string.IsNullOrEmpty(project_name)){
			Console.WriteLine("�v���W�F�N�g�����R�}���h���C�������Ɏw�肵�Ă��������B");
			return;
			}

		// �ݒ�t�@�C�������A�ǂݍ���
		string csprojPath = Path.Combine(project_name, "Assembly-CSharp.csproj");
		if (!File.Exists(csprojPath)){
			Console.WriteLine("�w�肳�ꂽ�p�X��csproj�t�@�C����������܂���B");
			return;
			}

		// XML�Ƃ��ă��[�h
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.Load(csprojPath);

		// ���O��ԃ}�l�[�W��
		XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);

		// ���O��Ԃ����݂���H
		string ns="//";
		if (xmlDoc.DocumentElement.NamespaceURI != ""){
			nsmgr.AddNamespace("ns", xmlDoc.DocumentElement.NamespaceURI);
			ns="//ns:";
			}

		// ���s
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
