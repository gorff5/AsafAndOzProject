<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>

<script language="javascript">

		    var GuID;
		    var myJSON_Ques;
		    var xmlHttp_OneTime;
		    var xmlHttp_Process;
		    var player;
		    
		    function myLoad() {
		        var player = JSON.parse(getCookie("playerCookie"));
		        GuID = player.GUID;
		        try {
		            xmlHttp_OneTime = new ActiveXObject("Microsoft.XMLHTTP");
		            xmlHttp_Process = new ActiveXObject("Microsoft.XMLHTTP");
		        }
		        catch (e) {
		            try {
		                xmlHttp_OneTime = new XMLHttpRequest()
		                xmlHttp_Process = new XMLHttpRequest()
		            }
		            catch (e) {
		            }
		        }
		        var url = "Handler.ashx?cmd=Register&guid=" + GuID;
		        xmlHttp_OneTime.open("POST", url, true);
		        xmlHttp_OneTime.onreadystatechange = getResponse_Connect;
		        xmlHttp_OneTime.send();
		
		    }

		    function getResponse_Connect() {
		        if (xmlHttp_OneTime.readyState == 4) {
		            if(xmlHttp_OneTime.responseText!="")
		            GuID = xmlHttp_OneTime.responseText;
		            ProcessFunction();
		        }
		    }

		    function ProcessFunction() {
		        var url = "Handler.ashx?cmd=Get&guid=" + GuID;
		        xmlHttp_Process.open("POST", url, true);
		        xmlHttp_Process.onreadystatechange = getResponse_Process;
		        xmlHttp_Process.send();
		    }
		    function getResponse_Process() {
		        if (xmlHttp_Process.readyState == 4) {
		            var myJSON_text = xmlHttp_Process.responseText;
		            myJSON_Ques = JSON.parse(myJSON_text);
		            if (myJSON_Ques != null) {
		                //youtube frame
		                var iframe = document.getElementById('video');
		                iframe.src = myJSON_Ques.url;
		                //buttons 
		                document.getElementById('Button1').value = myJSON_Ques.ANS_1.toString();
		                document.getElementById('Button2').value = myJSON_Ques.ANS_2.toString();
		                document.getElementById('Button3').value = myJSON_Ques.ANS_3.toString();
		                document.getElementById('Button4').value = myJSON_Ques.ANS_4.toString();
		                //correct ans
		                document.getElementById('Hidden1').value = myJSON_Ques.correct_ans.toString();
		            }
		            ProcessFunction();
		        }
		    }            	

		    function buttonClick(button) {
		        if (document.getElementById('Hidden1').value == button.value) {
		            var url = "Handler.ashx?cmd=Set&message=" + "aadDA" + "&user=" + "dsadffas";
		            xmlHttp_OneTime.open("POST", url, true);
		            xmlHttp_OneTime.send();
		        }
		    }


		    function myUnLoad() {
		        var url = "Handler.ashx?cmd=Unregister";
		        xmlHttp_OneTime.open("POST", url, true);
		        xmlHttp_OneTime.send();
		    }

		    function getCookie(name) {
		        var value = "; " + document.cookie;
		        var parts = value.split("; " + name + "=");
		        if (parts.length == 2) return parts.pop().split(";").shift();
		    }

		    window.onload = myLoad;
		    window.onunload = myUnLoad;
		</script>
 
<body>
    <form id="form1" runat="server">
      <asp:Panel ID="Panel1" runat="server">
            <ContentTemplate>
                <h1>Embedded Video Example</h1>
                <p>The following video provides an introduction to WebMatrix:</p>
                <iframe
                    id="video"
                    width="560"
                    height="315"
                    src="//www.youtube.com/embed/3d5ZxZdEUvA?showinfo=0&controls=0"
                    frameborder="0"
                    allowfullscreen>
                </iframe>
                       <p>
                 <asp:Button ID="Button1" runat="server" Text="Button1" />
                 <asp:Button ID="Button2" runat="server" Text="Button2" />
                   <p>
                 <asp:Button ID="Button3" runat="server" Text="Button3" />
                 <asp:Button ID="Button4" runat="server" Text="Button4" />
                 <input id="Hidden1" type="hidden" value="Button1" />
            </ContentTemplate>
   </asp:Panel>
        </form>
</body>
</html>