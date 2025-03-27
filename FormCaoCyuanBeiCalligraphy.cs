using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * 
*/


namespace CaoCyuanBeiCalligraphy
{
    public partial class FormCaoCyuanBeiCalligraphy : Form
    {
        Size m_PictureBoxDistSize = new Size(3000, 3000); //pictureBoxDist的尺寸
        PictureBox[] m_Pics = new PictureBox[1000]; //每一張字的圖片
        int m_PicsCount = 0; //目錄中取得多少張字圖片
        string[] m_PicChar = new string[1000]; //每一張字圖片的檔名中的書寫字，檔名最後一個字
        string[] m_PicStrokes = new string[1000]; //每一張字圖片的檔名中的筆畫，檔名倒數2-3個字(紀錄筆畫特徵)
        string m_Poetry = ""; //要顯示的詩
        DirectoryInfo m_PicFolder; //以DirectoryInfo來存放目錄資訊是很好的方法
        bool m_TextBoxReduce = false;

        public FormCaoCyuanBeiCalligraphy()
        {
            InitializeComponent();
            InitializePictureBoxDistSize();
            InitializePics();
            SetFormSize();
            //panelImages.HorizontalScroll.Value = panelImages.HorizontalScroll.Maximum;
        }
        #region 初始化
        private void SetFormSize()
        {
            // Retrieve the working rectangle from the Screen class
            // using the PrimaryScreen and the WorkingArea properties.
            System.Drawing.Rectangle workingRectangle = Screen.PrimaryScreen.WorkingArea;
            // Set the size of the form slightly less than size of 
            // working rectangle.
            MinimumSize = new Size(1360, 360);
            //MaximumSize = new Size(1360, 720);
            //this.Size = new System.Drawing.Size(1360, 360);
            this.WindowState = FormWindowState.Maximized; //最大化
            //this.Size = new System.Drawing.Size(workingRectangle.Width - 10, workingRectangle.Height - 10);
            // Set the location so the entire form is visible.
            //Point newPosition = new Point(0, 0);
            //newPosition.X = (workingRectangle.Width - this.Width) / 2;
            //newPosition.Y = (workingRectangle.Height - this.Height) / 2;
            //this.Location = newPosition;
        }
        public void InitializePictureBoxDistSize() //初始化底圖尺寸
        {
            if (pictureBoxDist.Image == null)
            {
                pictureBoxDist.Image = new Bitmap(m_PictureBoxDistSize.Width, m_PictureBoxDistSize.Height);
                pictureBoxDist.Width = m_PictureBoxDistSize.Width;
                pictureBoxDist.Height = m_PictureBoxDistSize.Height;
                //return;
            }
            else
            {
                //pictureBoxDist.Dispose();
                pictureBoxDist.Image = new Bitmap(m_PictureBoxDistSize.Width, m_PictureBoxDistSize.Height);
                pictureBoxDist.Width = m_PictureBoxDistSize.Width;
                pictureBoxDist.Height = m_PictureBoxDistSize.Height;
            }
        }

        public void InitializePics() //讀取目錄下所有圖檔
        {
            for (int i = 0; i < m_Pics.Length; i++) //實體化每一張小圖
            {
                m_Pics[i] = new PictureBox();
                Controls.Add(m_Pics[i]);
                m_Pics[i].Visible = false;
                m_Pics[i].Location = new Point(1200, 0 + i * 10);
                m_Pics[i].Size = new Size(10, 10);
                m_Pics[i].ForeColor = Color.DodgerBlue;
                m_Pics[i].BorderStyle = BorderStyle.None;
            }

            DirectoryInfo tmpPath = new DirectoryInfo(textBox_Path.Text);
            //Console.WriteLine("圖片路徑：{0}",tmpPath.FullName);
            if (tmpPath.Exists)
            {
                GetFileNameList(tmpPath);
                buttonMergeWordPic_Click(default, EventArgs.Empty);
            }
        }


        #endregion

        #region 目錄檔案相關
        DirectoryInfo GetDirectoryInfo(string _path) //根據字串來取得目錄資料
        {
            try
            {
                using (FolderBrowserDialog dialog = new FolderBrowserDialog())
                {
                    dialog.Description = "請選取原始圖片目錄";
                    dialog.ShowNewFolderButton = true;
                    //dialog.RootFolder = Environment.SpecialFolder.m_Computer;
                    DirectoryInfo tmpPath = new DirectoryInfo(_path);
                    //Console.WriteLine(tmpPath.FullName);
                    if (tmpPath.Exists)
                    {
                        dialog.SelectedPath = tmpPath.FullName;
                    }
                    else
                    {
                        dialog.SelectedPath = System.Windows.Forms.Application.StartupPath;
                    }

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        //textBox_Path.Text = dialog.SelectedPath;
                        //m_PicFolder = new DirectoryInfo(@dialog.SelectedPath); //指定目錄位置，之後要提取或搬移、刪除都可以利用DirectoryInfo內建功能
                        //GetFileNameList(m_PicFolder); //取得此目錄下的所有檔案
                        return new DirectoryInfo(@dialog.SelectedPath); //指定目錄位置，之後要提取或搬移、刪除都可以利用DirectoryInfo內建功能
                        //foreach (string fileName in Directory.GetFiles(folder, "*.xml", SearchOption.TopDirectoryOnly))
                        //{
                        //    SQLGenerator.GenerateSQLTransactions(Path.GetFullPath(fileName));
                        //}
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("目錄不存在" + exc.Message + " , 請重新指定。");
                return null;
            }
        }

        void GetFileNameList(DirectoryInfo _directory) //取得該目錄中每一張圖檔名稱，並載入該圖檔到m_Pics[]
        {
            if (listBox_Files.Items.Count > 0)
                listBox_Files.Items.Clear();
            try
            {
                m_PicsCount = 0;
                SearchOption option = SearchOption.TopDirectoryOnly;
                //SearchOption option = SearchOption.AllDirectories;
                //包含找子目錄以下的檔案
                //if (!checkBox_IncludeSubDir.Checked)
                //    option = SearchOption.TopDirectoryOnly;
                string[] extensions = { "*.jpg", "*.jpeg", "*.gif", "*.png", "*.psd", "*.HEIC", "*.mp4", "*.mov" };
                var files = from file in GetFilesName(@_directory.FullName, extensions, option)
                //讀取檔案內容並判斷是否含有"Microsoft"字串
                //from line in File.ReadLines(file)
                //where line.Contains("Microsoft")

                    select new
                    {
                        //files 是檔名列表
                        //f.File 是全路徑檔名
                        File = file,
                        //Line = line
                    };

                m_PicsCount = files.Count(); //取得多少張圖

                if (m_PicsCount == 0) //該目錄沒有照片檔案
                {
                    //label_Status.Text = "該目錄無任何照片檔案！";
                    listBox_Files.Items.Add("該目錄無任何照片檔案！");
                    listBox_Files.Items.Add(" ");
                    return;
                }
                //更新listBox_Files清單內容，這只是偵錯用，沒有實際用途
                listBox_Files.BeginUpdate();
                int tmpCount = 0;
                foreach (var f in files) //從每一個照片檔案去取資料
                {
                    FileInfo tmpFile = new FileInfo(@f.File);
                    listBox_Files.Items.Add(tmpFile.FullName);
                    string tmpNameWithoutExt = Path.GetFileNameWithoutExtension(tmpFile.FullName);
                    m_PicChar[tmpCount] = tmpFile.Name.Substring(tmpNameWithoutExt.Length - 1, 1); //取出檔名的最後一個字，書寫字
                    m_PicStrokes[tmpCount] = tmpFile.Name.Substring(tmpNameWithoutExt.Length - 3, 2); //取出檔名的倒數3至2兩個字，筆畫
                    //Console.WriteLine(m_PicChar[tmpCount]);
                    LoadShowImage(tmpFile.FullName, m_Pics[tmpCount]);
                    tmpCount++;
                }
                //Console.WriteLine("{0} files found.", files.Count().ToString());
                //label_Status.Text = "該目錄下 " + m_PicsCount + " 檔案已搬移至新目錄。";
                listBox_Files.Items.Add("搬移結束");
                listBox_Files.Items.Add(" ");
                listBox_Files.TopIndex = listBox_Files.Items.Count - 1;
                listBox_Files.EndUpdate();
            }

            catch (UnauthorizedAccessException UAEx)
            {
                Console.WriteLine(UAEx.Message);
            }
            catch (PathTooLongException PathEx)
            {
                Console.WriteLine(PathEx.Message);
            }
        }
        //平行處理取得目錄下的所有檔案
        // Takes same patterns, and executes in parallel
        public static IEnumerable<string> GetFilesName(string path,
                            string[] searchPatterns,
                            SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return searchPatterns.AsParallel()
                   .SelectMany(searchPattern =>
                          System.IO.Directory.EnumerateFiles(path, searchPattern, searchOption));
        }
        #endregion

        void LoadShowImage(string _imageFullPathName, PictureBox _picBox) //載入圖片資料
        {
            //在此處載入一個新的圖片。
            Bitmap bitmap = new Bitmap(_imageFullPathName);
            // Stretches the image to fit the pictureBox.
            _picBox.SizeMode = PictureBoxSizeMode.AutoSize;
            _picBox.Image = (Image)bitmap;
        }


        private void GetPoetry() //取得詩句
        {
            switch (comboBoxFeature.SelectedItem.ToString())
            {
                case "波橫":
                    m_Poetry = GetPoetryByStrokes("波橫");
                    //ShowCharPics(m_Poetry);
                    break;
                case "短橫":
                    m_Poetry = GetPoetryByStrokes("短橫");
                    break;
                case "長豎":
                    m_Poetry = GetPoetryByStrokes("長豎");
                    break;
                case "豎彎鉤":
                    m_Poetry = GetPoetryByStrokes("彎鉤");
                    break;
                case "連折":
                    m_Poetry = GetPoetryByStrokes("連折");
                    break;
                case "斷折":
                    m_Poetry = GetPoetryByStrokes("斷折");
                    break;
                case "點點":
                    m_Poetry = GetPoetryByStrokes("點點");
                    break;
                case "旁糸":
                    m_Poetry = GetPoetryByStrokes("旁糸");
                    break;
                case "平捺":
                    m_Poetry = GetPoetryByStrokes("平捺");
                    break;
                case "波捺":
                    m_Poetry = GetPoetryByStrokes("波捺");
                    break;
                case "長撇":
                    m_Poetry = GetPoetryByStrokes("長撇");
                    break;
                case "豎撇":
                    m_Poetry = GetPoetryByStrokes("豎撇");
                    break;

                case "千字文":
                    m_Poetry = "天地玄黃宇宙洪荒\r\n日月盈昃辰宿列張\r\n寒來暑往秋收冬藏\r\n閏餘成歲律召調陽\r\n" +
                               "雲騰致雨露結為霜\r\n金生麗水玉出崑岡\r\n劍號巨闕珠稱夜光\r\n果珍李柰菜重芥薑\r\n" +
                               "海鹹河淡鱗潛羽翔\r\n龍師火帝鳥官人皇\r\n始制文字乃服衣裳\r\n推位讓國有虞陶唐\r\n" +
                               "弔民伐罪周發殷湯\r\n坐朝問道垂拱平章\r\n愛育黎首臣伏戎羌\r\n遐邇一體率賓歸王\r\n" +
                               "鳴鳳在樹白駒食場\r\n化被草木賴及萬方\r\n";
                    textBoxWords.Text = m_Poetry;
                    break;
                case "曹全碑":
                    m_Poetry = "君諱全，字景完\r\n敦煌效谷人也\r\n其先蓋周之胄\r\n武王秉乾之機\r\n" +
                               "翦伐殷商，既定爾勛\r\n福祿攸同\r\n封弟叔振鐸於曹國\r\n因氏焉，秦漢之際\r\n" +
                               "曹參夾輔王室\r\n世宗廓土斥竟\r\n子孫遷於雍州之郊\r\n分止右扶風\r\n" +
                               "或在安定，或處武都\r\n或居隴西，或家敦煌\r\n枝分葉布，所在為雄\r\n君高祖父敏\r\n" +
                               "舉孝廉，武威長史\r\n巴郡朐忍令，張掖居延都尉\r\n";
                    textBoxWords.Text = m_Poetry;
                    break;
                case "靜夜思":
                    //Console.WriteLine("靜夜思");
                    m_Poetry = "牀前明月光\r\n疑是地上霜\r\n舉頭望明月\r\n低頭思故鄉";
                    textBoxWords.Text = m_Poetry;
                    break;
                case "月下獨酌":
                    m_Poetry = "花間一壺酒\r\n獨酌無相親\r\n舉杯邀明月\r\n對影成三人\r\n月既不解飲\r\n影徒隨我身\r\n" +
                               "暫伴月將影\r\n行樂須及春\r\n我歌月徘徊\r\n我舞影零亂\r\n醒時同交歡\r\n醉後各分散\r\n" +
                               "永結無情遊\r\n相期邈雲漢";
                    textBoxWords.Text = m_Poetry;
                    break;
                case "黃鶴樓送孟浩然之廣陵":
                    m_Poetry = "故人西辭黃鶴樓\r\n煙花三月下揚州\r\n孤帆遠影碧空盡\r\n唯見長江天際流";
                    textBoxWords.Text = m_Poetry;
                    break;
                case "贈汪倫":
                    m_Poetry = "李白乘舟將欲行\r\n忽聞岸上踏歌聲\r\n桃花潭水深千尺\r\n不及汪倫送我情";
                    textBoxWords.Text = m_Poetry;
                    break;
                case "宣州謝朓樓餞別校書叔雲":
                    m_Poetry = "棄我去者\r\n昨日之日不可留\r\n亂我心者\r\n今日之日多煩憂\r\n" +
                               "長風萬里送秋雁\r\n對此可以酣高樓\r\n蓬萊文章建安骨\r\n中間小謝又清發\r\n" +
                               "俱懷逸興壯思飛\r\n欲上青天攬明月\r\n抽刀斷水水更流\r\n舉杯消愁愁更愁\r\n" +
                               "人生在世不稱意\r\n明朝散髮弄扁舟";
                    textBoxWords.Text = m_Poetry;
                    break;
                case "送友人":
                    m_Poetry = "青山橫北郭\r\n白水繞東城\r\n此地一爲別\r\n孤蓬萬里征\r\n" +
                               "浮雲遊子意\r\n落日故人情\r\n揮手自茲去\r\n蕭蕭班馬鳴";
                    textBoxWords.Text = m_Poetry;
                    break;
                case "將進酒":
                    m_Poetry = "君不見黃河之水天上來\r\n奔流到海不復回\r\n君不見高堂明鏡悲白髮\r\n朝如青絲暮成雪\r\n" +
                               "人生得意須盡歡\r\n莫使金樽空對月\r\n天生我才必有用\r\n千金散盡還復來\r\n" +
                               "烹羊宰牛且為樂\r\n會須一飲三百杯\r\n" +
                               "岑夫子 丹丘生\r\n將進酒 杯莫停\r\n與君歌一曲\r\n請君為我傾耳聽\r\n" +
                               "鐘鼓饌玉不足貴\r\n但願長醉不願醒\r\n古來聖賢皆寂寞\r\n唯有飲者留其名\r\n" +
                               "陳王昔時宴平樂\r\n斗酒十千恣讙謔\r\n主人為何言少錢\r\n徑須沽取對君酌\r\n" +
                               "五花馬 千金裘\r\n呼兒將出換美酒\r\n與爾同消萬古愁";
                    textBoxWords.Text = m_Poetry;
                    break;
                case "長相思":
                    m_Poetry = "長相思，在長安\r\n絡緯秋啼金井闌\r\n微霜悽悽簟色寒\r\n孤燈不明思欲絕\r\n" +
                               "卷帷望月空長嘆\r\n美人如花隔雲端\r\n" +
                               "上有青冥之高天\r\n下有淥水之波瀾\r\n天長路遠魂飛苦\r\n夢魂不到關山難\r\n" +
                               "長相思，摧心肝\r\n日色慾盡花含煙\r\n月明欲素愁不眠\r\n趙瑟初停鳳凰柱\r\n" +
                               "蜀琴欲奏鴛鴦弦\r\n此曲有意無人傳\r\n" +
                               "願隨春風寄燕然\r\n憶君迢迢隔青天\r\n" +
                               "昔時橫波目\r\n今作流淚泉\r\n不信妾腸斷\r\n歸來看取明鏡前\r\n" +
                               "美人在時花滿堂\r\n美人去後花餘牀\r\n牀中繡被卷不寢\r\n至今三載聞餘香\r\n" +
                               "香亦竟不滅\r\n人亦竟不來\r\n相思黃葉落\r\n白露溼青苔";
                    textBoxWords.Text = m_Poetry;
                    break;
                case "望廬山瀑布":
                    m_Poetry = "日照香爐生紫煙\r\n遙看瀑布掛前川\r\n飛流直下三千尺\r\n疑是銀河落九天";
                    textBoxWords.Text = m_Poetry;
                    break;
                case "行路難":
                    m_Poetry = "金樽清酒鬥十千\r\n玉盤珍羞直萬錢\r\n停杯投箸不能食\r\n拔劍四顧心茫然\r\n" +
                               "欲渡黃河冰塞川\r\n將登太行雪滿山\r\n閒來垂釣碧溪上\r\n忽復乘舟夢日邊\r\n" +
                               "行路難！行路難！\r\n多歧路，今安在？\r\n長風破浪會有時\r\n直掛雲帆濟滄海";
                    textBoxWords.Text = m_Poetry;
                    break;
                case "獨坐敬亭山":
                    m_Poetry = "眾鳥高飛盡\r\n孤雲獨去閒\r\n相看兩不厭\r\n只有敬亭山";
                    textBoxWords.Text = m_Poetry;
                    break;
                case "清平調":
                    m_Poetry = "雲想衣裳花想容\r\n春風拂檻露華濃\r\n若非羣玉山頭見\r\n會向瑤臺月下逢\r\n\r\n" +
                               "一枝紅艷露凝香\r\n雲雨巫山枉斷腸\r\n借問漢宮誰得似\r\n可憐飛燕倚新妝\r\n\r\n" +
                               "名花傾國兩相歡\r\n常得君王帶笑看\r\n解釋春風無限恨\r\n沉香亭北倚闌杆";
                    textBoxWords.Text = m_Poetry;
                    break;
                case "怨情":
                    m_Poetry = "美人卷珠簾\r\n深坐顰蛾眉\r\n但見淚痕溼\r\n不知心恨誰";
                    textBoxWords.Text = m_Poetry;
                    break;
                case "早發白帝城":
                    m_Poetry = "朝辭白帝彩雲間\r\n千里江陵一日還\r\n兩岸猿聲啼不住\r\n輕舟已過萬重山";
                    textBoxWords.Text = m_Poetry;
                    break;
                case "送別(李叔同)":
                    m_Poetry = "長亭外，古道邊\r\n芳草碧連天\r\n晚風拂柳笛聲殘\r\n夕陽山外山\r\n" +
                               "天之涯，地之角\r\n知交半零落\r\n一瓢濁酒盡餘歡\r\n今宵別夢寒\r\n" +
                               "長亭外，古道邊\r\n芳草碧連天\r\n問君此去幾時來\r\n來時莫徘徊\r\n" +
                               "天之涯，地之角\r\n知交半零落\r\n人生難得是歡聚\r\n唯有別離多";
                    textBoxWords.Text = m_Poetry;
                    break;
                case "定風波(蘇東坡)":
                    m_Poetry = "莫聽穿林打葉聲\r\n何妨吟嘯且徐行\r\n" +
                               "竹杖芒鞋輕勝馬\r\n誰怕\r\n一蓑煙雨任平生\r\n" +
                               "料峭春風吹酒醒\r\n微冷\r\n山頭斜照卻相迎\r\n" +
                               "回首向來蕭瑟處\r\n歸去\r\n也無風雨也無晴\r\n";
                    textBoxWords.Text = m_Poetry;
                    break;
                default:
                    break;
                    /*
                    波橫
                    短橫
                    長豎
                    豎彎鉤
                    連折
                    斷折
                    點點
                    旁糸
                    平捺
                    波捺
                    長撇
                    豎撇
                    千字文
                    靜夜思
                    月下獨酌
                    黃鶴樓送孟浩然之廣陵
                    贈汪倫
                    宣州謝朓樓餞別校書叔雲
                    送友人
                    將進酒
                    長相思
                    望廬山瀑布
                    行路難
                    獨坐敬亭山
                    清平調
                    怨情
                    早發白帝城
                    送別(李叔同)
                    定風波(蘇東坡)
                    */
            }
            panelImages.HorizontalScroll.Value = panelImages.HorizontalScroll.Maximum;
            ShowCharPics(m_Poetry);

        }

        private string GetPoetryByStrokes(string _keyWord) //透過字圖片群的檔名倒數2-3個字(紀錄筆畫特徵)，
        {
            if (m_PicStrokes[0] == "")
            {
                return "字圖片尚未分析或錯誤";
            }
            string tmpSt = "";
            for (int i = 0; i < m_PicsCount; i++)
            {
                //Console.WriteLine(_keyWord + " 筆畫：" + m_PicStrokes[i] + " " + m_PicChar[i]);
                if (m_PicStrokes[i] == _keyWord)
                {
                    tmpSt += m_PicChar[i].Substring(m_PicChar[i].Length - 1, 1);
                }

                if (tmpSt.Length == 8)
                {
                    tmpSt += Environment.NewLine;
                }
                else if (tmpSt.Length > 10 && tmpSt.Length % 10 == 8)
                {
                    tmpSt += Environment.NewLine;
                }

            }
            //Console.WriteLine("這種筆畫有那幾個字：" + tmpSt);
            return tmpSt;
        }

        void ShowCharPics(string _poetry) //在pictureBoxDist顯示詩句中的文字
        {
            if (_poetry != "")
            {
                using (Graphics grD = Graphics.FromImage(pictureBoxDist.Image))
                {
                    //清除image
                    //Graphics grD = Graphics.FromImage(pictureBoxDist.Image);
                    grD.Clear(Color.FromArgb(255,28,34,28)); //清除前景顏色
                    //grD.Dispose();
                    //pictureBoxDist.Refresh();

                    //清除背景
                    //grD.Clear(Color.White);
                    //grD.Dispose();
                    //pictureBoxDist.Refresh();

                    //去掉image，显示出背景
                    //pictureBoxDist.Image = null;
                    //pictureBoxDist.Refresh();

                    //設定字體與顏色
                    Font tmpFontLi = new Font("JinMeiMaoLiShuFlower", (int)((float)numericUpDownPatternSize.Value * 0.6)); //設定隸書體
                    Font tmpFontKai = new Font("標楷體", (int)((float)numericUpDownPatternSize.Value * 0.6)); //設定楷書體
                    //Brush tmpBrush = new SolidColorBrush(color));
                    SolidBrush m_sbrushLi = new SolidBrush(Color.FromArgb(255, 200, 200, 160));
                    SolidBrush m_sbrushKai = new SolidBrush(Color.FromArgb(128, 80, 80, 255));
                    SolidBrush m_sbrushKaiSolid = new SolidBrush(Color.FromArgb(255, 80, 80, 255));

                    int tmpVertical = 0; //直行有效字的數量，避開NewLine等等符號
                    int tmpHNewLine = 0; //有幾個NewLine符號
                    bool tmpFindPicCharOr = false; //有沒有找到符合字的圖

                    for (int i = 0; i < _poetry.Length; i++) //一個字一個字顯示
                    {
                        if (_poetry.Length > i + 2 && _poetry.Substring(i, 2) == Environment.NewLine) //換行符號
                        {
                            i++;
                            tmpHNewLine++;
                            tmpVertical = 0;
                            //Console.WriteLine("New Line");
                        }
                        else
                        {
                            int tmpPicCount = 0; //第幾張圖片
                            tmpFindPicCharOr = false;
                            foreach (var item in m_PicChar) //根據每一張圖片來
                            {
                                if (item == _poetry.Substring(i, 1))
                                {
                                    grD.DrawImage(m_Pics[tmpPicCount].Image,
                                        new Rectangle(m_PictureBoxDistSize.Width - (tmpHNewLine + 1) * (int)numericUpDownPatternSize.Value,
                                        tmpVertical * (int)numericUpDownPatternSize.Value,
                                        (int)numericUpDownPatternSize.Value,
                                        (int)numericUpDownPatternSize.Value),
                                        new Rectangle(0, 0, m_Pics[tmpPicCount].Width, m_Pics[tmpPicCount].Height),
                                        GraphicsUnit.Pixel);
                                    if (checkBoxDisplayKai.Checked) //顯示上層楷書
                                    {
                                        //再顯示楷書，用來比較兩種字體的差異
                                        grD.TextRenderingHint = TextRenderingHint.AntiAlias; //防鋸齒狀
                                        grD.DrawString(_poetry.Substring(i, 1), tmpFontKai, m_sbrushKai,
                                            new Point(m_PictureBoxDistSize.Width - (tmpHNewLine + 1) * (int)numericUpDownPatternSize.Value - (int)((float)numericUpDownPatternSize.Value * 0.05),
                                            tmpVertical * (int)numericUpDownPatternSize.Value + (int)((float)numericUpDownPatternSize.Value * 0.1)));
                                    }
                                    tmpFindPicCharOr = true;
                                    tmpPicCount++;
                                    break;
                                }
                                tmpPicCount++;
                            }
                            if (!tmpFindPicCharOr) //沒找到字的圖片，就顯示字串
                            {
                                if (checkBoxDisplayKai.Checked)//顯示楷書，用來比較兩種字體的差異，先顯示一層完全不透明的藍色字
                                {
                                    //grD.TextRenderingHint = TextRenderingHint.SingleBitPerPixel; //不防鋸齒狀
                                    grD.DrawString(_poetry.Substring(i, 1), tmpFontKai, m_sbrushKaiSolid,
                                    new Point(m_PictureBoxDistSize.Width - (tmpHNewLine + 1) * (int)numericUpDownPatternSize.Value - (int)((float)numericUpDownPatternSize.Value * 0.05),
                                    tmpVertical * (int)numericUpDownPatternSize.Value + (int)((float)numericUpDownPatternSize.Value * 0.1)));
                                }
                                //先顯示隸書
                                grD.TextRenderingHint = TextRenderingHint.AntiAlias; //防鋸齒狀
                                grD.DrawString(_poetry.Substring(i, 1), tmpFontLi, m_sbrushLi,
                                    new Point(m_PictureBoxDistSize.Width - (tmpHNewLine + 1) * (int)numericUpDownPatternSize.Value - (int)((float)numericUpDownPatternSize.Value * 0.05),
                                    tmpVertical * (int)numericUpDownPatternSize.Value + (int)((float)numericUpDownPatternSize.Value * 0.1)));
                                if (checkBoxDisplayKai.Checked)//顯示楷書，用來比較兩種字體的差異，先顯示一層半透明的藍色字
                                {
                                    //再顯示楷書，用來比較兩種字體的差異
                                    //grD.TextRenderingHint = TextRenderingHint.SingleBitPerPixel; //不防鋸齒狀
                                    grD.DrawString(_poetry.Substring(i, 1), tmpFontKai, m_sbrushKai,
                                    new Point(m_PictureBoxDistSize.Width - (tmpHNewLine + 1) * (int)numericUpDownPatternSize.Value - (int)((float)numericUpDownPatternSize.Value * 0.05),
                                    tmpVertical * (int)numericUpDownPatternSize.Value + (int)((float)numericUpDownPatternSize.Value * 0.1)));
                                }
                            }
                            tmpVertical++;
                        }
                    }
                }
                //Console.WriteLine(pictureBoxDist.AutoScrollOffset);
                //pictureBoxDist.AutoScrollOffset = new Point(1000,0); //沒用，ScrollBar不會移動
                pictureBoxDist.Refresh();
            }
        }

        #region 按鍵
        private void buttonFolderBrowser_Click(object sender, EventArgs e) //指定目錄位置，若成功，讀取目錄中所有圖片檔案名稱
        {
            DirectoryInfo tmpDirInfo = GetDirectoryInfo(textBox_Path.Text);
            if (tmpDirInfo != null)
            {
                m_PicFolder = tmpDirInfo;
                textBox_Path.Text = m_PicFolder.FullName;
                GetFileNameList(m_PicFolder); //取得此目錄下的所有檔案
            }
        }

        private void buttonMergeWordPic_Click(object sender, EventArgs e) //顯示圖片
        {
            ShowCharPics(textBoxWords.Text); //根據textBoxWords.Text內容顯示圖片
            /*
            if (pictureBoxDist.Image == null)
            {
                pictureBoxDist.Image = new Bitmap(1870,3000);
                pictureBoxDist.Width = 1870;
                pictureBoxDist.Height = 3000;
                //return;
            }
            else
            {
                //pictureBoxDist.Dispose();
                pictureBoxDist.Image = new Bitmap(1870, 3000);
                pictureBoxDist.Width = 1870;
                pictureBoxDist.Height = 3000;
            }

            if (textBoxWords.Text != "")
            {
                using (Graphics grD = Graphics.FromImage(pictureBoxDist.Image))
                {
                    Font tmpFont = new Font("JinMeiMaoLiShuFlower", (int)((float)numericUpDownPatternSize.Value * 0.66));
                    //Brush tmpBrush = new SolidColorBrush(color));
                    SolidBrush m_sbrush1 = new SolidBrush(Color.FromArgb(200,200,160));
                    int tmpVertical = 0; //直行有效字的數量，避開NewLine等等符號
                    int tmpHNewLine = 0; //有幾個NewLine符號
                    bool tmpFindPicCharOr = false; //有沒有找到符合字的圖
                    for (int i = 0; i < textBoxWords.Text.Length; i++)
                    {
                        if (textBoxWords.Text.Length > i+2 && textBoxWords.Text.Substring(i, 2) == Environment.NewLine) //換行符號
                        {
                            i++;
                            tmpHNewLine++;
                            tmpVertical = 0;
                            Console.WriteLine("New Line");
                        }
                        else
                        {
                            int tmpPicCount = 0; //第幾張圖片
                            tmpFindPicCharOr = false;
                            foreach (var item in m_PicChar) 
                            {
                                if (item == textBoxWords.Text.Substring(i,1))
                                {
                                    grD.DrawImage(m_Pics[tmpPicCount].Image,
                                        new Rectangle(1800 - (tmpHNewLine + 1) * (int)numericUpDownPatternSize.Value, 
                                        tmpVertical * (int)numericUpDownPatternSize.Value,
                                        (int)numericUpDownPatternSize.Value,
                                        (int)numericUpDownPatternSize.Value), 
                                        new Rectangle(0,0, m_Pics[tmpPicCount].Width, m_Pics[tmpPicCount].Height), 
                                        GraphicsUnit.Pixel);
                                    tmpFindPicCharOr = true;
                                    tmpPicCount++;
                                    break;
                                }
                                tmpPicCount++;
                            }
                            if (!tmpFindPicCharOr) //沒找到字的圖片，就顯示字串
                            {
                                grD.DrawString(textBoxWords.Text.Substring(i, 1), tmpFont, m_sbrush1,
                                    new Point(1800 - (tmpHNewLine + 1) * (int)numericUpDownPatternSize.Value - (int)((float)numericUpDownPatternSize.Value*0.1),
                                    tmpVertical * (int)numericUpDownPatternSize.Value + (int)((float)numericUpDownPatternSize.Value * 0.05)));
                            }
                            tmpVertical++;
                        }
                    }
                }
            }
            */
        }

        private void buttonReduceTextBox_Click(object sender, EventArgs e) //讓文字輸入框收縮或延展
        {
            if(m_TextBoxReduce)
            {
                textBoxWords.Height = 1000;
                m_TextBoxReduce = false;
            }
            else
            {
                textBoxWords.Height = 27;
                m_TextBoxReduce = true;

            }
        }
        private void textBoxWords_TextChanged(object sender, EventArgs e) //偵測textBoxWords變更時，也同步更新m_Poetry
        {
            m_Poetry = textBoxWords.Text;
            buttonMergeWordPic_Click(default,EventArgs.Empty);
        }


        private void comboBoxFeature_SelectIndexChange(object sender, EventArgs e) //選擇comboBoxFeature，選筆畫或詩詞
        {
            GetPoetry();
        }

        private void checkBoxDisplayKai_CheckedChanged(object sender, EventArgs e) //是否顯示楷書
        {
            buttonMergeWordPic_Click(default, EventArgs.Empty);
        }
        #endregion


        Point latestPoint = new Point();
        int panHorizontalScroll;
        int panVerticalScroll;

        Point panellatestPoint = new Point();

        private void FormCaoCyuanBeiCalligraphy_Load(object sender, EventArgs e)
        {

        }
        private void pictureBoxDist_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // Remember the location where the button was pressed
                latestPoint = e.Location;
            }
            else if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
            {
                // Remember the location where the button was pressed
                latestPoint = e.Location;
                panHorizontalScroll = panelImages.HorizontalScroll.Value;
                panVerticalScroll = panelImages.VerticalScroll.Value;
                //Console.WriteLine("panelImages.VerticalScroll.Value = " + panelImages.VerticalScroll.Value);
                //Console.WriteLine("panelImages.HorizontalScroll.Value = " + panelImages.HorizontalScroll.Value);
                //latestPoint.X = panelImages.VerticalScroll.Value;
                //latestPoint.Y = panelImages.HorizontalScroll.Value;
            }

            //Console.WriteLine("MouseDown" + latestPoint.X + " " + latestPoint.Y);

        }

        private void pictureBoxDist_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                using (Graphics g = pictureBoxDist.CreateGraphics())
                {
                    // Draw next line and...
                    g.DrawLine(Pens.Red, latestPoint, e.Location);

                    // ... Remember the location
                    latestPoint = e.Location;
                }
            }
            else if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
            {
                //這個方法會亂跳
                //Console.WriteLine("e.Location.Y A " + e.Location.Y);
                Point epoint = e.Location;
                int tmpX = panelImages.HorizontalScroll.Value - (epoint.X - latestPoint.X);
                int tmpY = panelImages.VerticalScroll.Value - (epoint.Y - latestPoint.Y);
                //int tmpX = panelImages.HorizontalScroll.Value + (e.Location.X - latestPoint.X);
                //int tmpY = panelImages.VerticalScroll.Value + (e.Location.Y - latestPoint.Y);
                //int tmpX = panelImages.HorizontalScroll.Value + (latestPoint.X - e.Location.X);
                //int tmpY = panelImages.VerticalScroll.Value + (latestPoint.Y - e.Location.Y);
                if (tmpX >= panelImages.HorizontalScroll.Minimum && tmpX <= panelImages.HorizontalScroll.Maximum)
                {
                    //panelImages.HorizontalScroll.Value += e.Location.X - latestPoint.X;
                    panelImages.HorizontalScroll.Value = panHorizontalScroll - (e.Location.X - latestPoint.X);
                    //panelImages.HorizontalScroll.Value = tmpX;
                }
                if (tmpY >= panelImages.VerticalScroll.Minimum && tmpY <= panelImages.VerticalScroll.Maximum)
                {
                    //panelImages.VerticalScroll.Value += e.Location.Y - latestPoint.Y;
                    //panelImages.VerticalScroll.Value = tmpY;
                    //Console.WriteLine("MouseMove " + panelImages.VerticalScroll.Value);
                }
                //latestPoint = epoint;
                //panelImages.Refresh();
                Console.WriteLine("panelImages.HorizontalScroll.Value = " + panelImages.HorizontalScroll.Value);
            }
            //Console.WriteLine("MouseDown" + latestPoint.X + " " + latestPoint.Y);

        }

        private void panelImages_MouseDown(object sender, MouseEventArgs e)
        {
            //if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
            //{
            //    // Remember the location where the button was pressed
            //    panellatestPoint = e.Location;
            //    //latestPoint.X = panelImages.VerticalScroll.Value;
            //    //latestPoint.Y = panelImages.HorizontalScroll.Value;
            //}

        }

        private void panelImages_MouseMove(object sender, MouseEventArgs e)
        {
            //if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
            //{
            //    //這個方法會亂跳
            //    int tmpY = panelImages.VerticalScroll.Value + (e.Location.Y - panellatestPoint.Y);
            //    if (tmpY >= panelImages.VerticalScroll.Minimum && tmpY <= panelImages.VerticalScroll.Maximum)
            //    {
            //        panelImages.VerticalScroll.Value += e.Location.Y - panellatestPoint.Y;

            //    }
            //}

        }

        private void FormCaoCyuanBeiCalligraphy_Resize(object sender, EventArgs e)
        {
            panelImages.Size = new Size(Size.Width, Size.Height - 80);
        }
    }
}
