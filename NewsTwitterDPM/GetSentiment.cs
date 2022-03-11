using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace NewsTwitterDPM
{
    public static class GetSentiment
    {
        public static string RunFinBertCmd(List<string> ArticleTitles)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"PythonFiles", "FinBertTest.py");
            //string cmd = "/bin/bash";
            ProcessStartInfo start = new ProcessStartInfo
            {
        
                FileName = "/usr/bin/python3",
                Arguments = $"{path}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardInput = true
            };
            using Process process = Process.Start(start);
            using (StreamWriter swStandardInput = process.StandardInput)
            {
                if (swStandardInput.BaseStream.CanWrite)
                {
                    foreach (var title in ArticleTitles)
                    {
                        swStandardInput.WriteLine(title);
                    }
                }
            }
            using StreamReader reader = process.StandardOutput;
            string result = reader.ReadToEnd();
            return result;
        }
        
        public static string RunTweetBert(List<string> ArticleTitles)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"PythonFiles", "TweetBertTest.py");
            //string cmd = "/bin/bash";
            ProcessStartInfo start = new ProcessStartInfo
            {
        
                FileName = "/usr/bin/python3",
                Arguments = $"{path}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardInput = true
            };
            using Process process = Process.Start(start);
            using (StreamWriter swStandardInput = process.StandardInput)
            {
                if (swStandardInput.BaseStream.CanWrite)
                {
                    foreach (var title in ArticleTitles)
                    {
                        swStandardInput.WriteLine(title);
                    }
                }
            }
            using StreamReader reader = process.StandardOutput;
            string result = reader.ReadToEnd();
            return result;
        }
    }
}