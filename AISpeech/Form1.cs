using Baidu.Aip.Speech;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AISpeech
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //选择文件按钮
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "C# Corner Open File Dialog";
            //fdlg.InitialDirectory = @"c:/";   //@是取消转义字符的意思
            //fdlg.Filter = "All files（*.*）|*.*|All files(*.*)|*.* ";
            ///*
            // * FilterIndex 属性用于选择了何种文件类型,缺省设置为0,系统取Filter属性设置第一项
            // * ,相当于FilterIndex 属性设置为1.如果你编了3个文件类型，当FilterIndex ＝2时是指第2个.
            // */
            fdlg.FilterIndex = 2;
            ///*
            // *如果值为false，那么下一次选择文件的初始目录是上一次你选择的那个目录，
            // *不固定；如果值为true，每次打开这个对话框初始目录不随你的选择而改变，是固定的  
            // */
            //fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                //textBox1.Text = System.IO.Path.GetFileNameWithoutExtension(fdlg.FileName);
                filePath.Text = Path.GetFullPath(fdlg.FileName);

                videoType.Text = Path.GetExtension(fdlg.FileName);//后缀名(视频类型)
            }
        }
        /// <summary>
        /// 开始识别按钮
        /// 
        /// dev_pid 可选参数
        /// 1536    普通话(支持简单的英文识别)  搜索模型 无标点 支持自定义词库 http://vop.baidu.com/server_api
        /// 1537    普通话(纯中文识别)  输入法模型 有标点 不支持自定义词库 http://vop.baidu.com/server_api
        /// 1737    英语 无标点 不支持自定义词库 http://vop.baidu.com/server_api
        /// 1637    粤语 有标点 不支持自定义词库 http://vop.baidu.com/server_api
        /// 1837    四川话 有标点 不支持自定义词库 http://vop.baidu.com/server_api
        /// 1936    普通话远场 远场模型    有标点 不支持自定义词库    http://vop.baidu.com/server_api
        /// 80001语音识别极速版(收费)
        /// 80001   普通话 极速版输入法模型    有标点 支持自定义词库 http://vop.baidu.com/pro_api
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            var type = videoType.Text;
            var filePath = this.filePath.Text;

            //设置语音识别应用的账号信息(百度智能云管理中心) APP_ID/API_KEY/SECRET_KEY
            //demo里的账号
            //var APP_ID = "14433392";
            //var API_KEY = "C7WMYgLeWv3Wm2yogwv5gD08";
            //var SECRET_KEY = "xcvwiwikALBDBaIcGisNQ6aQImtj3qua";
            //请更改成自己的账号
            string APP_ID = "16982575";
            string API_KEY = "0kC4dDwWl3hqo2xz2c4113ZP";
            string SECRET_KEY = "8aneCyn9KVAWjGKAIfpcr3vMCFt19kIb";

            var client = new Asr(APP_ID, API_KEY, SECRET_KEY);
            client.Timeout = 120000; // 若语音较长，建议设置更大的超时时间. ms

            // 可选参数
            var options = new Dictionary<string, object>
                 {
                    {"dev_pid", 1536}
                 };

            var result = client.Recognize(File.ReadAllBytes(filePath), type.TrimStart('.'), 16000, options);

            voiceResult.Text = Convert.ToString(result);
            result.TryGetValue("result", out JToken resultStr);
            if (Convert.ToString(resultStr) != "")
                voiceResult.Text = Convert.ToString(resultStr);
        }
    }
}
