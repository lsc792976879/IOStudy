using System;
using System.IO;
using System.Net;
using System.Text;

namespace IOTest;

class IOTest
{
    static void Main(string[] args)
    {
        //1、加@为路径
        string filePath = @"C:\Users\lsc\Desktop\C#学习\IO\dataFolder\data.txt";
        
        //2、从文件路径中，读取文件夹路径、文件名，拓展名
        var extendName = Path.GetExtension(filePath);
        var fileName = Path.GetFileNameWithoutExtension(filePath);
        var folderPath = Path.GetDirectoryName(filePath);
        Console.WriteLine("文件路径为:{0}\n文件名为:{1}\n拓展名为:{2}",folderPath,fileName,extendName);
        
        //3、判断文件夹、文件是否存在，没有则创建
        if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
        if (!File.Exists(filePath)) File.Create(filePath);
        
        //4、读取文件信息
        FileInfo fileInfo = new FileInfo(filePath);
        Console.WriteLine("\n文件名:{0},\n文件路径{1},\n文件大小{2}B,\n创建日期{3}",fileInfo.Name,fileInfo.Directory,fileInfo.Length,fileInfo.CreationTime);
        
        //5、读取文件夹信息
        DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
        long len = 0;
        foreach (var fi in directoryInfo.GetFiles())
        {
            len += fi.Length;
        }
        Console.WriteLine("\n文件夹名:{0},\n文件夹路径{1},\n文件夹大小{2}B,\n创建日期{3}",directoryInfo.Name,directoryInfo.FullName,len,directoryInfo.CreationTime);

        //以流的方式写入数据
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            for (int i = 0; i < 10; i++)
            {
                sw.WriteLine("这是用StreamWriter写入的第" + i + "条数据");
            }
        }
        
        using (FileStream fs = File.Open(filePath,FileMode.Append))
        {
            byte[] buffer = new byte[1024];
            for (int i = 0; i < 10; i++)
            {
                buffer = Encoding.UTF8.GetBytes("这是用filestream添加的第" + i + "条数据\n");
                fs.Write(buffer);   
            }
        }

        //以流的方式读出数据
        /*using (FileStream fs = File.OpenRead(filePath))
        {
            byte[] buffer = new byte[1024];
            fs.Read(buffer, 0, buffer.Length);
            Console.WriteLine("\n-----------------------------------------------------------------");
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine("\n用filestream读取文件内容...文件内容如下：\n");
            Console.WriteLine(Encoding.UTF8.GetString(buffer));
        }*/

        using (StreamReader sr = new StreamReader(filePath))
        {
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine("\n用streamReader读取文件内容...文件内容如下：\n");
            while (!sr.EndOfStream)
            {
                Console.WriteLine(sr.ReadLine());
            }
        }
        
        
    }
}