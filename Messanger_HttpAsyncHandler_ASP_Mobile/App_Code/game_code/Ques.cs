using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// question object quntians info on the question
/// </summary>
[Serializable]
public class Ques
{
    public string url = "http://www.youtube.com/embed/XGSy3_Czz8k?showinfo=0&controls=0";
    public string ANS_1;
    public string ANS_2;
    public string ANS_3;
    public string ANS_4;
    public string correct_ans;
    public string guid;

    public Ques()
    {
    }
    public Ques(String url,String ANS_1,String ANS_2,String ANS_3,String ANS_4,String correct_ans)
    {
        this.url = url;
        this.ANS_1 = ANS_1;
        this.ANS_2 = ANS_2;
        this.ANS_3 = ANS_3;
        this.ANS_4 = ANS_4;
        this.correct_ans = correct_ans;
	}
}
