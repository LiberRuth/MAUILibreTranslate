using MAUILibreTranslate;
using System.Diagnostics;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;


namespace MAUILibreTranslate;

public partial class MainPage : ContentPage
{
	LibreTranslateSharp translateSharp = new LibreTranslateSharp();
    List<language_list> language_Lists = new List<language_list>();
    List<instance_list> public_Instances = new List<instance_list>();

    public MainPage()
	{
		InitializeComponent();
      
        language_code();
        inTranslate.SelectedIndex = language_Lists.Count - 1;
        enTranslate.SelectedIndex = 0;
        public_instance();
        //translateSharp.server = "https://translate.argosopentech.com/translate";
        //translateSharp.server = "https://translate.terraprint.co/translate";
    }

    private class language_list
    {
        public string language { get; set; }
        public string id { get; set; }

        public override string ToString()
        {
            return id;
        }
    }

    private class instance_list
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public override string ToString()
        {
            return Url;
        }
    }

    private async void public_instance()
    {
        public_Instances.Add(new instance_list() { Name = "translate.terraprint.co", Url = "https://translate.terraprint.co/translate" });
        public_Instances.Add(new instance_list() { Name = "translate.argosopentech.com", Url = "https://translate.argosopentech.com/translate" });
        server_list.ItemsSource = public_Instances;

        string IndexToken = await SecureStorage.Default.GetAsync("server_Index");
        server_list.SelectedIndex = Convert.ToInt32(IndexToken);

        if (IndexToken == null)
        {
            server_list.SelectedIndex = 0;
        }
    }


    private void language_code() 
    {
        language_Lists.Add(new language_list() { language = "Arabic", id = "ar" });
        language_Lists.Add(new language_list() { language = "Azerbaijani", id = "az" });
        language_Lists.Add(new language_list() { language = "Catalan", id = "ca" });
        language_Lists.Add(new language_list() { language = "Chinese", id = "zh" });
        language_Lists.Add(new language_list() { language = "Czech", id = "cs" });
        language_Lists.Add(new language_list() { language = "Danish", id = "da" });
        language_Lists.Add(new language_list() { language = "Dutch", id = "nl" });
        language_Lists.Add(new language_list() { language = "English", id = "en" });
        language_Lists.Add(new language_list() { language = "Esperanto", id = "eo" });
        language_Lists.Add(new language_list() { language = "Finnish", id = "fi" });
        language_Lists.Add(new language_list() { language = "French", id = "fr" });
        language_Lists.Add(new language_list() { language = "German", id = "de" });
        language_Lists.Add(new language_list() { language = "Greek", id = "el" });
        language_Lists.Add(new language_list() { language = "Hebrew", id = "he" });
        language_Lists.Add(new language_list() { language = "Hindi", id = "hi" });
        language_Lists.Add(new language_list() { language = "Hungarian", id = "hu" });
        language_Lists.Add(new language_list() { language = "Indonesian", id = "id" });
        language_Lists.Add(new language_list() { language = "Irish", id = "ga" });
        language_Lists.Add(new language_list() { language = "Italian", id = "it" });
        language_Lists.Add(new language_list() { language = "Japanese", id = "ja" });
        language_Lists.Add(new language_list() { language = "Korean", id = "ko" });
        language_Lists.Add(new language_list() { language = "Persian", id = "fa" });
        language_Lists.Add(new language_list() { language = "Polish", id = "pl" });
        language_Lists.Add(new language_list() { language = "Portuguese", id = "pt" });
        language_Lists.Add(new language_list() { language = "Russian", id = "ru" });
        language_Lists.Add(new language_list() { language = "Slovak", id = "sk" });
        language_Lists.Add(new language_list() { language = "Spanish", id = "es" });
        language_Lists.Add(new language_list() { language = "Swedish", id = "sv" });
        language_Lists.Add(new language_list() { language = "Turkish", id = "tr" });
        language_Lists.Add(new language_list() { language = "Ukranian", id = "uk" });

        enTranslate.ItemsSource = language_Lists;

        language_Lists.Add(new language_list() { language = "자동", id = "auto" });
        inTranslate.ItemsSource = language_Lists;
    }

    private async void TranslationBtn_Clicked(object sender, EventArgs e)
    {
        Debug.WriteLine(server_list.SelectedItem.ToString());
        translateSharp.server = server_list.SelectedItem.ToString();
        await SecureStorage.Default.SetAsync("server_Index", server_list.SelectedIndex.ToString());

        string text = "";

        CounterBtn.IsEnabled = false;
        inTranslate.IsEnabled = false;
        enTranslate.IsEnabled = false;
        
        await Task.Run(async () =>
        {
            translateSharp.txtinput = ineditor.Text;
            translateSharp.source = inTranslate.SelectedItem.ToString();
            translateSharp.target = enTranslate.SelectedItem.ToString();
            await Task.Delay(1000);
            text = translateSharp.Translate();

        });
        eneditor.Text = text;
        CounterBtn.IsEnabled = true; //UWP bug?
        inTranslate.IsEnabled = true;
        enTranslate.IsEnabled = true;

    }

    private async void Button_inCopy(object sender, EventArgs e)
    {
        await Clipboard.Default.SetTextAsync(ineditor.Text);
    }

    private void Button_inClear(object sender, EventArgs e)
    {
        ineditor.Text = "";
    }

    private async void Button_enCopy(object sender, EventArgs e)
    {
        await Clipboard.Default.SetTextAsync(eneditor.Text);
    }

    private void Button_enClear(object sender, EventArgs e)
    {
        eneditor.Text = "";
    }

}

