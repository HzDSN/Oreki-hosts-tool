using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ConsoleApplication1
{
    class Program
    {
        int count = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to use Oreki hosts Tool 2.0!");
            Console.WriteLine("欢迎使用 Oreki hosts Tool 2.0");
            Console.WriteLine("If it takes too many times than usual, exit and retry.");
            Console.WriteLine("如果用时比通常更长，请退出后重试");
            Console.WriteLine("STEP 1: Downloading hosts...");
            Console.WriteLine("第 1 步：下载 hosts...");
            string hosts = download();
            if (hosts.Length >= 1000)
            {
                if (hosts.Substring(0, 11) == "# Copyright")
                {
                    Console.WriteLine("STEP 2: Replacing hosts file...");
                    Console.WriteLine("第 2 步：替换 hosts 文件");
                    FileStream stream = new FileStream(Environment.GetEnvironmentVariable("systemdrive") + @"\Windows\system32\drivers\etc\hosts", FileMode.Create);
                    StreamWriter str = new StreamWriter(stream);
                    str.WriteLine(hosts);
                    str.Close();
                    Console.WriteLine("hosts file set successfully!");
                    Console.WriteLine("hosts 文件设置成功！");
                    MessageBox.Show("hosts file set successfully!\r\nhosts 文件设置成功！", "Oreki hosts Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                else
                {
                    Console.WriteLine("Download failed!!!");
                    Console.WriteLine("下载失败！！！");
                }
            }

            else
            {
                Console.WriteLine("Download failed!!!");
                Console.WriteLine("下载失败！！！");
            }



        }

        static string download()
        {
            WebClient MyWebClient = new WebClient();
            MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于对向Internet资源的请求进行身份验证的网络凭据。
            Byte[] pageData = MyWebClient.DownloadData("https://github.com/racaljk/hosts/raw/master/hosts"); //从指定网站下载数据
            string pageHtml = Encoding.Default.GetString(pageData);  //获取网站页面            
            pageHtml += "\r\n\r\n\r\n\r\n#Google Play\r\n";
            pageData = MyWebClient.DownloadData("https://raw.githubusercontent.com/sy618/hosts/master/p"); //从指定网站下载数据
            string temp = Encoding.Default.GetString(pageData);
            temp = temp.Substring(25);
            pageHtml += temp;  
            pageHtml += "\r\n\r\n\r\n\r\n#YouTube\r\n";
            pageData = MyWebClient.DownloadData("https://raw.githubusercontent.com/sy618/hosts/master/y"); //从指定网站下载数据
            temp = Encoding.Default.GetString(pageData);
            temp = temp.Substring(28);
            pageHtml += temp;
            pageHtml += "\r\n\r\n\r\n\r\n\r\n";
            return (pageHtml);
        }
    }
}
