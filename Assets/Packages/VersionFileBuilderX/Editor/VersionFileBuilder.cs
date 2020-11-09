using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public class VersionFileBuilder : IPreprocessBuildWithReport, IPostprocessBuildWithReport
{
    #region Field

    public static readonly string FileName = "Version.txt";

    #endregion Field

    #region Property

    public int callbackOrder { get { return 0; } }

    #endregion Property

    #region Method

    public void OnPreprocessBuild(BuildReport buildReport)
    {
        // NOTE:
        // Alert when the existing version is newr than build version.

        string filePath = Path.GetDirectoryName(buildReport.summary.outputPath) + "\\" + FileName;

        if (!File.Exists(filePath))
        {
            return;
        }

        TextFileIOResult result = TextFileReadWriter.ReadFromFile(filePath);

        if (result.success)
        {
            Version previousVersion = new Version(result.text);
            Version buildVersion    = new Version(PlayerSettings.bundleVersion);

            if (previousVersion > buildVersion)
            {
                bool ok = EditorUtility.DisplayDialog(typeof(VersionFileBuilder).Name,
                                                     "Existing" + FileName + " is newer than this build version.",
                                                     "Build",
                                                     "Cancel");
                if (!ok)
                {
                    throw new BuildFailedException("This build was cancelled in " + typeof(VersionFileBuilder).Name);
                }
            }
        }
    }

    public void OnPostprocessBuild(BuildReport buildReport)
    {
        // NOTE:
        // BuildReport sometime gets Unknown even if Succeeded.
        // in Unity ver.2018/2019.

        if (buildReport.summary.result == (BuildResult.Cancelled | BuildResult.Failed))
        {
            return;
        }

        string filePath = Path.GetDirectoryName(buildReport.summary.outputPath) + "\\" + FileName;

        string version = new Version(PlayerSettings.bundleVersion).ToString();

        TextFileIOResult result = TextFileReadWriter.WriteToFile(filePath, version);

        if (!result.success)
        {
            EditorUtility.DisplayDialog(typeof(VersionFileBuilder).Name,
                                        "Faild to write Version.txt", "OK");
        }
    }

    #endregion Method
}