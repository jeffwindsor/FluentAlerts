using System;
using System.IO;

namespace FluentAlerts
{
    internal static class StringExtensions
    {
        public static string ExportToFile(this string json, string filePath)
        {
            //Backup Current File
            BackUpFileIfExists(filePath);

            // Write new contents
            File.WriteAllText(filePath, json);

            //Return name of back up file or empty
            return filePath;
        }

        private static void BackUpFileIfExists(this string filePath)
        {
            if (!File.Exists(filePath)) return;

            var dir = Path.GetDirectoryName(filePath);
            var backUpFileName = string.Format("{0}{1}BackupOf{2}_UTC{3}{4}",
                                               dir,
                                               (string.IsNullOrWhiteSpace(dir)) ? string.Empty : "\\",
                                               Path.GetFileNameWithoutExtension(filePath),
                                               DateTime.UtcNow.ToIsoFormat(),
                                               Path.GetExtension(filePath));

            File.Copy(filePath, backUpFileName);
        }
    }
}
