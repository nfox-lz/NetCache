using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows;

namespace Compete.NetCache
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new CacheClient("database"))
            {

            //client.SetStringValue("database", "collection", "abc", "123");
            //var a = client.GetStringValue("database", "collection", "abc");

            var bt = DateTime.Now;

                //for (int index = 10000; index < 20000; index++)
                //{
                //    var a = client.GetValue<Test>("collection", index.ToString());
                //}
                Parallel.For(0, 10000, index =>
                {
                    var a = client.GetValue<Test>("collection", index.ToString());
                });

                //var x = new Test1() { No = 10, Id = Guid.NewGuid(), Code = "10101", Name = "abc", CreateDateTime = DateTime.Now };
                ////x.X = x;
                //var y = new Test() { No = 10, Id = Guid.NewGuid(), Code = "10101", Name = "abc", CreateDateTime = DateTime.Now, X = x };

                //var now = DateTime.Now.Ticks.ToString();
                //var key = (new Random(Convert.ToInt32(now.Substring(now.Length - 9)))).Next().ToString();

                //Parallel.For(0, 10000, index =>
                //{
                //    client.SetValue("database", "collection", index.ToString(), y);
                //    //var a = client.GetValue<Test>("database", "collection", index.ToString());
                //});

                //Parallel.For(0, 10000, index =>
                //{
                //    var x = new Test1() { No = 10, Id = Guid.NewGuid(), Code = "10101", Name = "abc", CreateDateTime = DateTime.Now };
                //    //x.X = x;
                //    var y = new Test() { No = 10, Id = Guid.NewGuid(), Code = "10101", Name = "abc", CreateDateTime = DateTime.Now, X = x };

                //    var now = DateTime.Now.Ticks.ToString();
                //    var key = (new Random(Convert.ToInt32(now.Substring(now.Length - 9)))).Next().ToString();

                //    client.SetValue("database", "collection", index.ToString(), y);
                //    var a = client.GetValue<Test>("database", "collection", index.ToString());
                //});

                //client.Save();

                //for (int i = 0; i < 10000; i++)
                //{
                //    var x = new Test1() { No = 10, Id = Guid.NewGuid(), Code = "10101", Name = "abc", CreateDateTime = DateTime.Now };
                //    //x.X = x;
                //    var y = new Test() { No = 10, Id = Guid.NewGuid(), Code = "10101", Name = "abc", CreateDateTime = DateTime.Now, X = x };

                //    var now = DateTime.Now.Ticks.ToString();
                //    var key = (new Random(Convert.ToInt32(now.Substring(now.Length - 9)))).Next().ToString();

                //    client.SetValue("database", "collection", key, y);
                //    var a = client.GetValue<Test>("database", "collection", key);
                //}

                //Parallel.For(0, 1000, async index =>
                //{
                //    var x = new Test1() { No = 10, Id = Guid.NewGuid(), Code = "10101", Name = "abc", CreateDateTime = DateTime.Now };
                //    //x.X = x;
                //    var y = new Test() { No = 10, Id = Guid.NewGuid(), Code = "10101", Name = "abc", CreateDateTime = DateTime.Now, X = x };

                //    var now = DateTime.Now.Ticks.ToString();
                //    var key = (new Random(Convert.ToInt32(now.Substring(now.Length - 9)))).Next().ToString();

                //    client.SetValueAsync("database", "collection", key, y);
                //    var a = await client.GetValueAsync<Test>("database", "collection", key);
                //});
                //for (int i = 0; i < 1000; i++)
                //{
                //    var x = new Test1() { No = 10, Id = Guid.NewGuid(), Code = "10101", Name = "abc", CreateDateTime = DateTime.Now };
                //    //x.X = x;
                //    var y = new Test() { No = 10, Id = Guid.NewGuid(), Code = "10101", Name = "abc", CreateDateTime = DateTime.Now, X = x };

                //    var now = DateTime.Now.Ticks.ToString();
                //    var key = (new Random(Convert.ToInt32(now.Substring(now.Length - 9)))).Next().ToString();

                //    client.SetValueAsync("database", "collection", key, y);
                //    var a = await client.GetValueAsync<Test>("database", "collection", key);
                //}

                var et = DateTime.Now;

            MessageBox.Show(string.Format("{0}\r\n{1}\r\n{2}", bt, et, et - bt));
            }

            //client.SetBinValueAsync("database", "collection", "1", Guid.NewGuid().ToByteArray());
            //client.SetBinValue("database", "collection", "1", Guid.NewGuid().ToByteArray());


            //var x = new Test() { Id = 10, Code = "10101", Name = "abc" };
            //if ((x.GetType().Attributes & TypeAttributes.Serializable) == TypeAttributes.Serializable)
            //    MessageBox.Show("OK");
            //else
            //    MessageBox.Show("Error");

            //byte[] data;
            //using (var stream = new MemoryStream())
            //{
            //    var formatter = new BinaryFormatter();
            //    formatter.Serialize(stream, new Test() { Id = 10, Code = "10101", Name = "abc" });
            //    data = stream.GetBuffer();
            //    stream.Close();
            //}
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var client = new CacheClient("database");
            {
                var x = new Test1() { No = 10, Id = Guid.NewGuid(), Code = "10101", Name = "abc", CreateDateTime = DateTime.Now };
                //x.X = x;
                var y = new Test() { No = 10, Id = Guid.NewGuid(), Code = "10101", Name = "abc", CreateDateTime = DateTime.Now, X = x };

                var now = DateTime.Now.Ticks.ToString();
                var key = (new Random(Convert.ToInt32(now.Substring(now.Length - 9)))).Next().ToString();

                client.SetValue("collection", key, y);
                var a = client.GetValue<Test>("collection", key);
            }
        }
    }

    //[Serializable]
    public class Test
    {
        public long No { get; set; }

        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public DateTime CreateDateTime { get; set; }

        public Test1 X { get; set; }
    }

    public class Test1
    {
        public long No { get; set; }

        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
