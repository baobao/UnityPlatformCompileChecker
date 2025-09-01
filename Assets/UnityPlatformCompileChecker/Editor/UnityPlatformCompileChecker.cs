using System.IO;
using UnityEditor;
using UnityEditor.Build.Player;
using UnityEngine;

namespace Shibuya24.Editor
{
    public static class UnityPlatformCompileChecker
    {
        private const string MenuRoot = "Tools/CompileChecker/";

        private static readonly string ResultDir = "Temp/CompileCheckerResult";

        [MenuItem(MenuRoot + "iOS", priority = 1)]
        public static void CheckCompileiOS()
            => CheckCompile(BuildTarget.iOS, BuildTargetGroup.iOS);

        [MenuItem(MenuRoot + "Android", priority = 2)]
        public static void CheckCompileAndroid()
            => CheckCompile(BuildTarget.Android, BuildTargetGroup.Android);

        [MenuItem(MenuRoot + "iOS & Android", priority = 3)]
        public static void CheckCompileSmartPhone()
        {
            CheckCompile(BuildTarget.Android, BuildTargetGroup.Android);
            CheckCompile(BuildTarget.iOS, BuildTargetGroup.iOS);
        }

        [MenuItem(MenuRoot + "WebGL", priority = 4)]
        public static void CheckCompileWebGL()
            => CheckCompile(BuildTarget.WebGL, BuildTargetGroup.WebGL);

        [MenuItem(MenuRoot + "MacOS", priority = 5)]
        public static void CheckCompileMac()
            => CheckCompile(BuildTarget.StandaloneOSX, BuildTargetGroup.Standalone);

        [MenuItem(MenuRoot + "Windows64bit", priority = 6)]
        public static void CheckCompileWindows64()
            => CheckCompile(BuildTarget.StandaloneWindows64, BuildTargetGroup.Standalone);

        [MenuItem(MenuRoot + "Windows32bit", priority = 7)]
        public static void CheckCompileWindows32()
            => CheckCompile(BuildTarget.StandaloneWindows, BuildTargetGroup.Standalone);

        public static void CheckCompile(BuildTarget buildTarget, BuildTargetGroup buildTargetGroup,
            bool isDeleteResultDir = true)

        {
            var option = new ScriptCompilationSettings
            {
                target = buildTarget,
                group = buildTargetGroup
            };

            // Start Script Compilation
            if (IsCompileSuccess(PlayerBuildInterface.CompilePlayerScripts(option, ResultDir)))
            {
                Debug.Log($"<color=lime>Compile SUCCESS BuildTarget: {option.target}</color>");
            }
            else
            {
                Debug.LogError($"<color=red>Compile Fail BuildTarget: {option.target}</color>");
            }

            // delete result dir
            if (isDeleteResultDir && Directory.Exists(ResultDir))
            {
                Directory.Delete(ResultDir, true);
            }
        }

        private static bool IsCompileSuccess(ScriptCompilationResult result)
            => result.assemblies != null && result.assemblies.Count > 0 && result.typeDB != null;
    }
}