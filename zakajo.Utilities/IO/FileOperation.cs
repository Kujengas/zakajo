using System;
using System.IO;
using System.Text;

namespace zakajo.Utilities.IO
{
    public static class FileOperation
    {
        public static FileOperationResponse WriteFile(string input,string path)
        {
            StringBuilder outputText = new StringBuilder();
            try
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(input);
                }

                // Open the file to read from.
                using (StreamReader sr = File.OpenText(path))
                {
                    string s;
                    while ((s = sr.ReadLine()) != null)
                    {
                        outputText.AppendLine(s);
                    }
                }

            }
            catch (Exception ex) {
                return new FileOperationResponse { IsSuccess = false, OutputText = ex.Message, InputText = input };
            }

            return new FileOperationResponse { IsSuccess = true, OutputText = outputText.ToString(), InputText = input };
        }

        public static FileOperationResponse ReadFile(string path)
        {
            StringBuilder outputText = new StringBuilder();
            try
            {
                if (!File.Exists(path))
                {
                    throw new Exception("Cant find the specified filename or the file does not exist");
                }

                // Open the file to read from.
                using (StreamReader sr = File.OpenText(path))
                {
                    string s;
                    while ((s = sr.ReadLine()) != null)
                    {
                        outputText.AppendLine(s);
                    }
                }

            }
            catch (Exception ex)
            {
                return new FileOperationResponse { IsSuccess = false, OutputText = ex.Message, InputText = string.Empty };
            }

            return new FileOperationResponse { IsSuccess = true, OutputText = outputText.ToString(), InputText = string.Empty };
        }
    }
}
