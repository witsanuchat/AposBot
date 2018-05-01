<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotifyMutiple.aspx.cs" Inherits="Line_Bot_Application2.NotifyMutiple" %>

<!DOCTYPE html>

<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-alpha.6/css/bootstrap.min.css" integrity="sha384-rwoIResjU2yc3z8GV/NPeZWAv56rSmLldC3R/AZzGRnGxQQKnKkoFVhFQhNUwEyJ" crossorigin="anonymous">
<script src="https://code.jquery.com/jquery-3.1.1.slim.min.js" integrity="sha384-A7FZj7v+d/sdmMqp/nOQwliLvUsJfDHW+k9Omg/a/EheAdgtzNs3hpfag6Ed950n" crossorigin="anonymous"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/tether/1.4.0/js/tether.min.js" integrity="sha384-DztdAPBWPRXSA/3eYEEUWrWCy7G5KFbe8fFjk5JAIxUYHKkDx6Qin1DkWx51bBrb" crossorigin="anonymous"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-alpha.6/js/bootstrap.min.js" integrity="sha384-vBWWzlZJ8ea9aCX4pEW3rVHjgjt7zpkNpZk+02D9phzyeVkE+jo0ieGizqPLForn" crossorigin="anonymous"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>

<script src="https://cdn.datatables.net/1.10.16/js/jquery.dataTables.min.js"></script>
<script src="https://cdn.datatables.net/1.10.16/js/dataTables.bootstrap4.min.js"></script>
<script src="https://cdn.datatables.net/responsive/2.2.0/js/dataTables.responsive.min.js"></script>
<script src="https://cdn.datatables.net/responsive/2.2.0/js/responsive.bootstrap4.min.js"></script>
<script src="https://cdn.datatables.net/1.10.16/css/dataTables.bootstrap4.min.css"></script>
<script src="https://cdn.datatables.net/responsive/2.2.0/css/responsive.bootstrap4.min.css"></script>

<style type="text/css">
    .center_div {
        margin: auto;
        width: 90%;
        background-color: #00CCFF;
    }
</style>

<body>
    <script type="text/javascript">

        function ReaderSplit(pp) {
            arr = [];
            pp.replace(/\[(.*?)\]/g, function (s, match) { arr.push(match); });
            return arr;
        }
        $(document).ready(function () {
            $('#GridView1').DataTable();
        });
    </script>
    <script type="text/javascript">

        //<!--  -->
        //var ii = MoiApplet.getSubApplet().jInitialize();
        //	document.write(ii);
        document.write("<br>");
        var jListR = MoiApplet.getSubApplet().ListReader();
        var splitreader = ReaderSplit(jListR);
        MoiApplet.getSubApplet().SetReader(jListR);
        console.log("spl" + MoiApplet.getSubApplet().GetOffLineData());
        console.log("jse" + jListR);
        document.write("" + MoiApplet.getSubApplet().GetOffLineData());
        document.write(jListR);
        document.write("<br>");
        $.each(splitreader, function (key, value) {
            MoiApplet.getSubApplet().SetReader(jListR);
            //MoiApplet.getSubApplet().jOpenReader(value);
            var jpid = MoiApplet.getSubApplet().GetOffLineData();
            //var jcid = MoiApplet.getSubApplet().jGetCID();
            spid = jpid.split(":");
            //scid = jcid.split(":");
            document.write(spid[1]);
            document.write("<br>");

            //document.write(scid[1]);
            document.write("<br>");


        });
        MoiApplet.getSubApplet().jInitialize();
        //MoiApplet.getSubApplet().jCloseReader();
        MoiApplet.getSubApplet().jUnInitialize();


    </script>

    <form id="form1" runat="server">
        <div class="container-fleid" >
            <br />
              <div class ="row">
                   <div class="col-xs col-sm col-md col-lg col-xl-10 offset-1" style="border:2px solid #cecece;">
                  <h2 class="text-xl-center">เพิ่มการแจ้งเตือน</h2><br />
             
             <div class ="row">
                   <div class="col-xs col-sm col-md col-lg col-xl-1 offset-1">
                  <h5 class="text-xl-right">ข้อความ :</h5>
              </div>
                  <div class="col-xs col-sm col-md col-lg col-xl-4">
                <textarea  class="form-control" type="text"id="TBMessage" row="5"></textarea>
            </div>
                 <div class="col-xs col-sm col-md col-lg col-xl-1">
                  <h5 class="text-xl-right">ประเภท :</h5> 
                    
              </div>
                 <div class="col-xs col-sm col-md col-lg col-xl-4">
                  <asp:CheckBoxList class="CheckBoxList-control" ID="CheckBoxList1" runat="server" Height="182px" Width="298px">
                     <asp:ListItem>กยศ</asp:ListItem>
                     <asp:ListItem>ข่าวสารมหาลัย</asp:ListItem>
                     <asp:ListItem>ข่าวสารภายในคณะ</asp:ListItem>
                     <asp:ListItem>กองกิจ</asp:ListItem>
                     <asp:ListItem>หอพักชาย</asp:ListItem>
                     <asp:ListItem>หอพักหญิง</asp:ListItem>
                     </asp:CheckBoxList>
                     </div>
                  </div><br /></div>
                  </div>
            <br />

           
           
    



    </form>
</body>
</html>

