using LineMessagingAPISDK;
using LineMessagingAPISDK.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ThaiSplitLib;
using THSplit;

namespace Line_Bot_Application2.Controllers
{
    public static class Globals
    {
        public static string UserIDLine { get; set; }
        public static string StuIDs { get; set; }
    }
    public class Broker
    {
        //SqlConnection conn;
        //SqlConnectionStringBuilder connStringBuilder;


        void ConnectTo()
        {
           

            System.Data.SqlClient.SqlConnection _SqlConnection = new System.Data.SqlClient.SqlConnection();
            _SqlConnection.ConnectionString = "Server=tcp:aposbotserver.database.windows.net,1433;Initial Catalog=AposBOT;Persist Security Info=False;User ID=chatpont;Password=P.Apos!bot;MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";
            
           
        }
       
    }

    public class LineMessagesController : ApiController
    {
        

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post(HttpRequestMessage request)
        {
            /* if (!await VaridateSignature(request))
                 return Request.CreateResponse(HttpStatusCode.BadRequest);*/
           
            Activity activity = JsonConvert.DeserializeObject<Activity>
                (await request.Content.ReadAsStringAsync());
   
            // Line may send multiple events in one message, so need to handle them all.
            foreach (Event lineEvent in activity.Events)
            {
                LineMessageHandler handler = new LineMessageHandler(lineEvent);
                
                Profile profile = await handler.GetProfile(lineEvent.Source.UserId);

                switch (lineEvent.Type)
                {
                    case EventType.Beacon:
                        await handler.HandleBeaconEvent();
                        break;
                    case EventType.Follow:
                        await handler.HandleFollowEvent();
                        break;
                    case EventType.Join:
                        await handler.HandleJoinEvent();
                        break;
                    case EventType.Leave:
                        await handler.HandleLeaveEvent();
                        break;
                    case EventType.Message:
                        Message message = JsonConvert.DeserializeObject<Message>(lineEvent.Message.ToString());
                        switch (message.Type)
                        {
                            case MessageType.Text:
                                await handler.HandleTextMessage();
                                break;
                            case MessageType.Audio:
                            case MessageType.Image:
                            case MessageType.Video:
                                await handler.HandleMediaMessage();
                                break;
                            case MessageType.Sticker:
                                await handler.HandleStickerMessage();
                                break;
                            case MessageType.Location:
                                await handler.HandleLocationMessage();
                                break;
                        }
                        break;
                    case EventType.Postback:
                        await handler.HandlePostbackEvent();
                        break;
                    case EventType.Unfollow:
                        await handler.HandleUnfollowEvent();
                        break;
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        private async Task<bool> VaridateSignature(HttpRequestMessage request)
        {
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["ChannelSecret"].ToString()));
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(await request.Content.ReadAsStringAsync()));
            var contentHash = Convert.ToBase64String(computeHash);
            var headerHash = Request.Headers.GetValues("X-Line-Signature").First();

            return contentHash == headerHash;
        }
    }
    public class MulticastMessage
    {
        ControllMultiCastMs conMS = new ControllMultiCastMs();
       // private Event lineEvent;
        private LineClient lineClient = new LineClient(ConfigurationManager.AppSettings["ChannelToken"].ToString());
        
        public async Task<Profile> GetProfile(string mid)
        {
            return await lineClient.GetProfile(mid);
        }
        public async Task sentalluser(String AllMess)
        {
           
            await Reply(AllMess);
        }
        private async Task Reply(String AllMess)
        {


            List<ControllMultiCastMs> CallconMs = conMS.LoadAccount();
           
            List<string> AllAccount = new List<string>();

            foreach(ControllMultiCastMs cont in CallconMs)
            {



                AllAccount.Add(cont.AccountLine);
            }
            //var result = string.Join(",", CallconMs.ToArray());
            try
               
            {


                Console.WriteLine(AllAccount);
                await lineClient.MulticastAsync(AllAccount, new List<Message>() { new TextMessage(AllMess) });
            }
            catch
            {
                
            }
        }
    }
    public class LineMessageHandler
    {
        private Event lineEvent;
        private LineClient lineClient = new LineClient(ConfigurationManager.AppSettings["ChannelToken"].ToString());

        public LineMessageHandler(Event lineEvent)
        {
            this.lineEvent = lineEvent;
        }

        public async Task HandleBeaconEvent()
        {
           
        }

        public async Task HandleFollowEvent()
        {
        }

        public async Task HandleJoinEvent()
        {
        }

        public async Task HandleLeaveEvent()
        {
        }

        public async Task HandlePostbackEvent()
        {
            var replyMessage = new TextMessage(lineEvent.Postback.Data);
            await Reply(replyMessage);
        }

        public async Task HandleUnfollowEvent()
        {
        }

        public async Task<Profile> GetProfile(string mid)
        {
            return await lineClient.GetProfile(mid);
        }
        public async Task HandleMuticast()
        {

        }

        public async Task HandleTextMessage()
        {
            Select select = new Select();
            

            LineMessageHandler handler = new LineMessageHandler(lineEvent);
            var textMessage = JsonConvert.DeserializeObject<TextMessage>(lineEvent.Message.ToString());
            var UserID = (lineEvent.Source.UserId.ToString());
            Globals.UserIDLine = UserID;
            Message replyMessage = null;
            if (textMessage.Text.ToLower() == "menu")
            {

                List<TemplateAction> actions = new List<TemplateAction>();
                //actions.Add(new MessageTemplateAction("Grade", select.selectName()));
                actions.Add(new MessageTemplateAction("Grade", "grade"));
                actions.Add(new MessageTemplateAction("Class Schedule", "class"));
                actions.Add(new MessageTemplateAction("Registration Result", "registration"));
                actions.Add(new MessageTemplateAction("Help", "help"));
                ButtonsTemplate buttonsTemplate = new ButtonsTemplate("https://s-media-cache-ak0.pinimg.com/originals/ed/b9/4e/edb94ee7ef8e6c1b1dd176abe7b3af38.jpg", "Menu", "....", actions);
                replyMessage = new TemplateMessage("Menu", buttonsTemplate);
            }


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            else if (textMessage.Text.ToLower() == "help")
            {
                List<TemplateAction> actions = new List<TemplateAction>();
                actions.Add(new MessageTemplateAction("คำสั่งการใช้งานพื้นฐาน", "คำสั่งการใช้งานพื้นฐาน"));
                actions.Add(new UriTemplateAction("วิธีการลงทะเบียนเรียน", "https://www.youtube.com/watch?v=H_TlXBJbKCo"));

                ButtonsTemplate buttonsTemplate = new ButtonsTemplate("https://s-media-cache-ak0.pinimg.com/originals/ed/b9/4e/edb94ee7ef8e6c1b1dd176abe7b3af38.jpg", "AposBotHelp", "ช่วยเหลือผู้ใช้งาน", actions);

                replyMessage = new TemplateMessage("Help", buttonsTemplate);
            }
            

            else if (textMessage.Text.ToLower() == "คำสั่งการใช้งานพื้นฐาน")
            {
                replyMessage = new TextMessage("คำสั่งใช่งานพื้นฐาน AposBOT  1. 'Register' ลงทะเบียนยืนยันตัวตน   2. 'grade' แสดงข้อมูลเกรดแต่ล่ะเทอม   3. 'GPA' แสดง GPA รวมทั้งหมดถึงปจจุบัน  4. 'class' แสดงตารางเรียน     6. 'registration' แสดงผลการลงทะเบียนเรียน ");
            }
            else if (textMessage.Text.ToLower() == "ชื่ออะไร")
            {
                replyMessage = new TextMessage("ผมชื่อ AposBot ครับ เรียกสั่นๆว่า พอส ได้ครับ >///<");
            }
            else if (textMessage.Text.ToLower() == "พอส")
            {
                replyMessage = new TextMessage("มีอะไรให้รับใช้ครับ สามารถ เรียก menu การทำงานได้ครับ");
            }
            else if (textMessage.Text.ToLower() == "กินข้าวยัง" || textMessage.Text.ToLower() == "กินอะไรรึยัง" || textMessage.Text.ToLower() == "กินข้าว")
            {
                replyMessage = new TextMessage("ผมทาาแล้วครับ เป็น Code ผัดเผ็ด กับต้ม แซ่บ Code อร่อยเหาะ");
            }
            else if (textMessage.Text.ToLower() == "สวย" || textMessage.Text.ToLower() == "เราสวย" || textMessage.Text.ToLower() == "เราสวยไหม" || textMessage.Text.ToLower() == "สวยไหม")
            {
                replyMessage = new TextMessage("สวยมากมั๊กมากกก");
            }
            else if (textMessage.Text.ToLower() == "หล่อ" || textMessage.Text.ToLower() == "เราหล่อ" || textMessage.Text.ToLower() == "เราหล่อไหม" || textMessage.Text.ToLower() == "หล่อไหม")
            {
                replyMessage = new TextMessage("หล่อมั๊กมากกกก");
            }
            else if (textMessage.Text.ToLower() == "5" || textMessage.Text.ToLower() == "55" || textMessage.Text.ToLower() == "555" || textMessage.Text.ToLower() == "555" || textMessage.Text.ToLower() == "5555")
            {
                replyMessage = new TextMessage("หัวเราะใหญ่เลย >///<");
            }
            else if (textMessage.Text.ToLower() == "อ้าว")
            {
                replyMessage = new TextMessage("เฮ้ย");
            }
            else if (textMessage.Text.ToLower() == "ขอแคปชั่น")
            {
                replyMessage = new TextMessage("ที่ตั้งใจเรียนเพราะอยากมีอนาคต แต่ที่รักเธอไม่ลดเพราะอยากมีอนาคตกับเธอ");
            }
            else if (textMessage.Text.ToLower() == "บอทกินข้าวยัง")
            {
                replyMessage = new TextMessage("รอคนที่ถามพาไปหาข้าวกิน ฮ่าๆ");
            }
            else if (textMessage.Text.ToLower() == "รักมากแค่ไหน")
            {
                replyMessage = new TextMessage("มากเท่าใจที่มี");
            }
            else if (textMessage.Text.ToLower() == "อยู่ไหน")
            {
                replyMessage = new TextMessage("อยู่ในใจเธอไง");
            }
            else if (textMessage.Text.ToLower() == "เป็นแฟนกัน")
            {
                replyMessage = new TextMessage("รับบัตรคิวข้างหน้าเลยครับ");
            }
            else if (textMessage.Text.ToLower() == "เรากลัวติด E จังเลย")
            {
                replyMessage = new TextMessage("ไม่ติดหรอกเรารู้เธอเก่ง");
            }
            else if (textMessage.Text.ToLower() == "เธอว่าเทอมนี้เราจะสอบผ่านไหม")
            {
                replyMessage = new TextMessage("ถ้าเธอตั้งใจอ่านหนังสือ เราชื่อว่าเธอทำได้อยู่แล้ว");
            }
            else if (textMessage.Text.ToLower() == "ขอกำลังใจก่อนสอบหน่อย")
            {
                replyMessage = new TextMessage("ขอให้ทำข้อสอบได้นะครับ สู้ๆๆ");
            }
            else if (textMessage.Text.ToLower() == "มีเมียยัง")
            {
                replyMessage = new TextMessage("เป็นโหลๆๆๆๆๆๆๆๆๆ");
            }
            else if (textMessage.Text.ToLower() == "ขอบคุณนะที่เป็นกำลังใจให้เรา")
            {
                replyMessage = new TextMessage("ยินดีเสมอ");
            }
            else if (textMessage.Text.ToLower() == "พอจะมีซัก200ไหม")
            {
                replyMessage = new TextMessage("คุยได้ทุกเรื่องยกเว้นเรื่องเงิน");
            }
            else if (textMessage.Text.ToLower() == "มีเรื่องปรึกษา")
            {
                replyMessage = new TextMessage("ได้เลยครับ");
            }
            else if (textMessage.Text.ToLower() == "ยืมตังหน่อย")
            {
                replyMessage = new TextMessage("โห่เสียดายโดนยืมตัดหน้าซะและว่าจะยืมเธออยู่");
            }
            else if (textMessage.Text.ToLower() == "ไม่เหมือนที่คุยกันไว้นี้หน่า")
            {
                replyMessage = new TextMessage("ที่บอกว่าเธอจะเลือกเค้าและไม่มีวันกลับมา");
            }
            else if (textMessage.Text.ToLower() == "อย่ากลืนน้ำลายตัวเองดีกว่า")
            {
                replyMessage = new TextMessage("อย่ามัวเสียเวลาขอร้องอะไรที่ไม่มีวันได้คืน");
            }
            else if (textMessage.Text.ToLower() == "ทำไรอยู่")
            {
                replyMessage = new TextMessage("รอคุยกับเธอไง");
            }
            else if (textMessage.Text.ToLower() == "เล่นมุขหน่อย" || textMessage.Text.ToLower() == "เล่นมุข" || textMessage.Text.ToLower() == "บอสเล่นมุข")
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 10);
                string ans = "";
                switch (randomNumber)
                {
                    case 0: ans = "ช: เราเลิกกันเถอะผมรู้ว่าคุณรักผมน้อยลง ญ: ฉันไม่เคยรักคุณน้อยลงนะ ช: น้อยสิ เดือนก่อนคุณรักผม 31 วัน เดือนนี้คุณรักผม 30 วัน ญ: พ่อง"; break;
                    case 1: ans = "เมื่อปูกับหลานคุยกัน หลาน: ปู่ๆโทสับมีไลน์มั้ยฮับ ปู่: ของปู่ไม่มีลาย ปู่ชอบแบบเรียบๆๆ หลาน: @#%&*?!"; break;
                    case 2: ans = "A: เวลาเอ็งปิดประตูตู้เสื้อผ้า เอ็งปิดดังไหม B: ก็ไม่ดังมาก ทำไมวะ A: ปิดเบาๆทีหลังB : ไมวะ A : มีชุดนอนอยู่"; break;
                    case 3: ans = "A: วันก่อนครับ B: ทำไมวะ A: เปิดตู้เย็นที ล้มเลยครับ B: พื้นลื่นหรอวะ A: เจอซุปไก่สกัด...(แบรนด์)"; break;
                    case 4: ans = "ผม: วันก่อนไปป้าตี้กับเพื่อนมาเพื่อน: ทำมัยวะ ผม: อยู่ดีๆเพื่อนอีกคนก็นอนซั๊กเฉยเลย เพื่อน: อาวเป็นรัยวะ ผม: เบียร์สิง"; break;
                    case 5: ans = "ช:  เมื่อวันก่อนผมไปดูดวงมาคับ ญ: หราๆ ดูเรื่องอ่ะไรค่ะ ช: เนื้อคู่ คับ ญ: แล้วเป็นไงบ้างค่ะ ช: หมอดูบอกว่า เนื้อคู่ เป็นคนต่างชาติ คับ ญ: หรอๆ ชาติ ไหนค่ะ ช: ชาติน่าคับ เราชาตินี้เขาชาติ ช: กวนตีน.."; break;
                    case 6: ans = "A: ผมปวดท้องอ่ะครับ บอกทางไปห้องนํ้าหน่อย B: เดินไปเลี้ยวซ้าย A: มีทางที่เร็วกว่านั้นมั้ยครับ? B: วิ่งไปเลี้ยวซ้าย A : เนวาสยพ้ดำกีเท #@$%^*"; break;
                    case 7: ans = "ช: เห้ยๆเธอญ: ไรหรอ ช: มีอะไรติดหน้าเธออะ ญ: ไหนอะ ช: ผงผงอะ ญ: ผงไรอะ ช: ผงรักเธอครับ"; break;
                    case 8: ans = "A: วันก่อนครับทํษไมครับ B: ทําไมครับ A: ไปซื้อไก่ KFC ผมต่อแถวนะแต่ไม่รู้ทํษไมโดนด่า B: ใครด่าครับ A: ก็ตอนช่วยหยิบของให้ชาวต่างชาติข้างหน้าอะครับ B: เขาด่าว่าอะไร A: เขาบอกว่าแซงคิวแซงคิว B: เขาบอกว่า Thank you"; break;
                    case 9: ans = "ก: จังหวัดอะไรชอบวิ่ง ข: ไม่รู้ก:ตอบ บุรีรัน ข: ครับ"; break;
                    case 10: ans = "ผม: การเต้นอะไร สมาชิกในวงมาพร้อมกันทุกคน? เพื่อน: ไม่รู้วะ..ผม: .......การเต้น ครบเว่อร์! เพื่อน: ไอ่สั........."; break;
                }
                replyMessage = new TextMessage(ans);
            }
           
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            else if (textMessage.Text.ToLower() == "grade")
            {
                List<TemplateColumn> columns = new List<TemplateColumn>();

                List<TemplateAction> actions = new List<TemplateAction>();
                actions.Add(new MessageTemplateAction("1/2557", "1/2557"));
                actions.Add(new MessageTemplateAction("2/2557", "2/2557"));
                actions.Add(new MessageTemplateAction("3/2557", "3/2557"));
                //actions.GetEnumerator(new Message).actions();

                List<TemplateAction> actions2 = new List<TemplateAction>();
                actions2.Add(new MessageTemplateAction("1/2558", "1/2558"));
                actions2.Add(new MessageTemplateAction("2/2558", "2/2558"));
                actions2.Add(new MessageTemplateAction("3/2558", "3/2558"));


                List<TemplateAction> actions3 = new List<TemplateAction>();
                actions3.Add(new MessageTemplateAction("1/2559", "1/2559"));
                actions3.Add(new MessageTemplateAction("2/2559", "2/2559"));
                actions3.Add(new MessageTemplateAction("3/2559", "3/2559"));


                List<TemplateAction> actions4 = new List<TemplateAction>();
                actions4.Add(new MessageTemplateAction("1/2560", "1/2560"));
                actions4.Add(new MessageTemplateAction("2/2560", "2/2560"));
                actions4.Add(new MessageTemplateAction("3/2560", "3/2560"));



                columns.Add(new TemplateColumn() { Title = "ผลการเรียนประจำปี 2557", Text = "....", ThumbnailImageUrl = "https://s-media-cache-ak0.pinimg.com/originals/ed/b9/4e/edb94ee7ef8e6c1b1dd176abe7b3af38.jpg", Actions = actions });
                columns.Add(new TemplateColumn() { Title = "ผลการเรียนประจำปี 2558", Text = "....", ThumbnailImageUrl = "https://s-media-cache-ak0.pinimg.com/originals/ed/b9/4e/edb94ee7ef8e6c1b1dd176abe7b3af38.jpg", Actions = actions2 });
                columns.Add(new TemplateColumn() { Title = "ผลการเรียนประจำปี 2559", Text = "....", ThumbnailImageUrl = "https://s-media-cache-ak0.pinimg.com/originals/ed/b9/4e/edb94ee7ef8e6c1b1dd176abe7b3af38.jpg", Actions = actions3 });
                columns.Add(new TemplateColumn() { Title = "ผลการเรียนประจำปี 2560", Text = "....", ThumbnailImageUrl = "https://s-media-cache-ak0.pinimg.com/originals/ed/b9/4e/edb94ee7ef8e6c1b1dd176abe7b3af38.jpg", Actions = actions4 });
                CarouselTemplate carouselTemplate = new CarouselTemplate(columns);
                replyMessage = new TemplateMessage("Grade", carouselTemplate);

            }
            else if (textMessage.Text.ToLower() == "1/2557")
            {
                replyMessage = new TextMessage(select.selectGrade("1/2557"));
            }
            else if (textMessage.Text.ToLower() == "2/2557")
            {
                replyMessage = new TextMessage(select.selectGrade("2/2557"));
            }
            else if (textMessage.Text.ToLower() == "3/2557")
            {
                replyMessage = new TextMessage(select.selectGrade("3/2557"));
            }
            else if (textMessage.Text.ToLower() == "1/2558")
            {
                replyMessage = new TextMessage(select.selectGrade("1/2558"));
            }
            else if (textMessage.Text.ToLower() == "2/2558")
            {
                replyMessage = new TextMessage(select.selectGrade("2/2558"));
            }
            else if (textMessage.Text.ToLower() == "3/2558")
            {
                replyMessage = new TextMessage(select.selectGrade("3/2558"));
            }
            else if (textMessage.Text.ToLower() == "1/2559")
            {
                replyMessage = new TextMessage(select.selectGrade("1/2559"));
            }
            else if (textMessage.Text.ToLower() == "2/2559")
            {
                replyMessage = new TextMessage(select.selectGrade("2/2559"));
            }
            else if (textMessage.Text.ToLower() == "3/2559")
            {
                replyMessage = new TextMessage(select.selectGrade("3/2559"));
            }
            else if (textMessage.Text.ToLower() == "1/2560")
            {
                replyMessage = new TextMessage(select.selectGrade("1/2560"));
            }
            else if (textMessage.Text.ToLower() == "2/2560")
            {
                replyMessage = new TextMessage(select.selectGrade("2/2560"));
            }
            else if (textMessage.Text.ToLower() == "3/2560")
            {
                replyMessage = new TextMessage(select.selectGrade("3/2560"));
            }
            else if (textMessage.Text.ToLower() == "gpa")
            {
                replyMessage = new TextMessage(select.GetsumGPA());
            }
            else if (textMessage.Text.ToLower() == "mon")
            {
                replyMessage = new TextMessage(select.GetSchedule("Mon"));
            }
            else if (textMessage.Text.ToLower() == "tues")
            {
                replyMessage = new TextMessage(select.GetSchedule("Tues"));
            }
            else if (textMessage.Text.ToLower() == "wed")
            {
                replyMessage = new TextMessage(select.GetSchedule("Wed"));
            }
            else if (textMessage.Text.ToLower() == "thur")
            {
                replyMessage = new TextMessage(select.GetSchedule("Thur"));
            }
            else if (textMessage.Text.ToLower() == "fri")
            {
                replyMessage = new TextMessage(select.GetSchedule("Fri"));
            }
            else if (textMessage.Text.ToLower() == "sat")
            {
                replyMessage = new TextMessage(select.GetSchedule("Sat"));
            }
            else if (textMessage.Text.ToLower() == "sun")
            {
                replyMessage = new TextMessage(select.GetSchedule("Sun"));
            }



            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            else if (textMessage.Text.ToLower() == "regis1/2559")
            {
                replyMessage = new TextMessage(select.GetRegisteration("1/2559"));
            }
            else if (textMessage.Text.ToLower() == "regis2/2559")
            {
                replyMessage = new TextMessage(select.GetRegisteration("2/2559"));
            }
            else if (textMessage.Text.ToLower() == "class")
            {
                List<TemplateColumn> columns = new List<TemplateColumn>();

                List<TemplateAction> actions = new List<TemplateAction>();
                actions.Add(new MessageTemplateAction("วันจันทร์", "mon"));
                actions.Add(new MessageTemplateAction("วันอังคาร", "tues"));
                actions.Add(new MessageTemplateAction("วันพุธ", "wed"));

                List<TemplateAction> actions2 = new List<TemplateAction>();
                actions2.Add(new MessageTemplateAction("วันพฤหัสบดี", "thur"));
                actions2.Add(new MessageTemplateAction("วันศุกร์", "fri"));
                actions2.Add(new MessageTemplateAction("เสาร์", "sat"));

                List<TemplateAction> actions3 = new List<TemplateAction>();
                actions3.Add(new MessageTemplateAction("วันอาทิตย์", "sun"));
                actions3.Add(new MessageTemplateAction("Back menu", "menu"));
                actions3.Add(new MessageTemplateAction(".", "."));

                columns.Add(new TemplateColumn() { Title = "ตารางเรียน", Text = "....", ThumbnailImageUrl = "https://s-media-cache-ak0.pinimg.com/originals/ed/b9/4e/edb94ee7ef8e6c1b1dd176abe7b3af38.jpg", Actions = actions });
                columns.Add(new TemplateColumn() { Title = "ตารางเรียน", Text = "....", ThumbnailImageUrl = "https://s-media-cache-ak0.pinimg.com/originals/ed/b9/4e/edb94ee7ef8e6c1b1dd176abe7b3af38.jpg", Actions = actions2 });
                columns.Add(new TemplateColumn() { Title = "ตารางเรียน", Text = "....", ThumbnailImageUrl = "https://s-media-cache-ak0.pinimg.com/originals/ed/b9/4e/edb94ee7ef8e6c1b1dd176abe7b3af38.jpg", Actions = actions3 });
                CarouselTemplate carouselTemplate = new CarouselTemplate(columns);

                replyMessage = new TemplateMessage("Class", carouselTemplate);
            }



            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            else if (textMessage.Text.ToLower() == "registration")
            {

                List<TemplateAction> actions = new List<TemplateAction>();
                actions.Add(new MessageTemplateAction("ปีการศึกษา 1/2559", "regis1/2559"));
                actions.Add(new MessageTemplateAction("ปีการศึกษา 2/2559", "regis2/2559"));

                ButtonsTemplate buttonsTemplate = new ButtonsTemplate("https://s-media-cache-ak0.pinimg.com/originals/ed/b9/4e/edb94ee7ef8e6c1b1dd176abe7b3af38.jpg", "ผลการลงทะเบีน", "ข้อมูลการลงทะเบียน", actions);

                replyMessage = new TemplateMessage("Help", buttonsTemplate);
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            else if (textMessage.Text.ToLower() == "confirm")
            {
                List<TemplateAction> actions = new List<TemplateAction>();
                actions.Add(new MessageTemplateAction("Yes", "yes"));
                actions.Add(new MessageTemplateAction("No", "no"));
                ConfirmTemplate confirmTemplate = new ConfirmTemplate("Confirm Test", actions);
                replyMessage = new TemplateMessage("Confirm", confirmTemplate);
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            else if (textMessage.Text.ToLower() == "carousel")
            {
                List<TemplateColumn> columns = new List<TemplateColumn>();

                List<TemplateAction> actions = new List<TemplateAction>();
                actions.Add(new MessageTemplateAction("Message Label", "sample data"));
                //actions.Add(new PostbackTemplateAction("Postback Label", "Total data"));
                actions.Add(new UriTemplateAction("Uri Label", "https://github.com/kenakamu"));
                columns.Add(new TemplateColumn() { Title = "Casousel 1 Title", Text = "Casousel 1 Text", ThumbnailImageUrl = "https://github.com/apple-touch-icon.png", Actions = actions });
                columns.Add(new TemplateColumn() { Title = "Casousel 2 Title", Text = "Casousel 2 Text", ThumbnailImageUrl = "https://github.com/apple-touch-icon.png", Actions = actions });
                CarouselTemplate carouselTemplate = new CarouselTemplate(columns);
                replyMessage = new TemplateMessage("Carousel", carouselTemplate);
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            else if (textMessage.Text.ToLower() == "register")
            {
                replyMessage = new TextMessage("ท่านสามารถลงทะเบียนเพื่อยืนยันตัวตนได้ตามลิ้ง ด้านล่าง    http://aposbot.azurewebsites.net/WebAuthen.aspx");
                Globals.UserIDLine = UserID;

            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            else if (textMessage.Text.ToLower() == "สวัสดี")
            {

                replyMessage = new TextMessage("สวัสดีคุณ " + select.selectName() + " มีอะไรให้ฉันช่วยสามารถสอบถามได้เลยครับ เริ่มใช้งานง่ายๆโดยคำสั่ง menu เลยครับ");
            }
            else if (textMessage.Text.ToLower() == "ทำอะไรได้บ้าง")
            {

                replyMessage = new TextMessage("คุณสามารถเรียนรู้วิธีการใช้งานได้โดยผ่าน พิมพ์คำสั่ง Menu");

            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            else if (textMessage.Text.ToLower() == "user")
            {

                String connStr = "Server=tcp:aposbotserver.database.windows.net,1433;Initial Catalog=AposBOT;Persist Security Info=False;User ID=chatpont;Password=P.Apos!bot;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                SqlConnection conn = new SqlConnection(connStr);

                conn.Open();

                string query = "INSERT INTO [dbo].[Authen]([StudentID], [AcountLine]) VALUES('5730213071', '858956465989')";
                SqlCommand cmd = new SqlCommand(query);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;

                cmd.ExecuteNonQuery();

                conn.Close();
                replyMessage = new TextMessage(UserID);


            }
            else
            {
                Spliter spliter = new Spliter();
                var output = spliter.SegmentByDictionary(textMessage.Text.ToLower());
                int i = 0;
                string word = "";
                foreach (var variable in output)
                {
                    Console.WriteLine(variable);
                    word = word + output[i];
                    
                    i++;
                }
                replyMessage = new TextMessage(word);


                //for (int i = 0; i <= output.Count; i++)
                //{
                //    replyMessage = new TextMessage(output[i]);
                //}
                //foreach (var variable in output)
                //{
                //    Console.WriteLine(variable);
                //    //replyMessage = new TextMessage("0."+output[0] +"1."+ output[1] + "2." + output[2] + "3." + output[3] + "4." + output[4] + "5." + output[5] + "6." + output[6]);
                //    replyMessage = new TextMessage(output[]);
                //}


                Random random = new Random();
                int randomNumber = random.Next(0, 11);
                string ans = "";
                switch (randomNumber)
                {
                    case 0: ans = "มันคืออะไรหรอครับ"; break;
                    case 1: ans = "ผมไม่เข้าใจค๊าบบบ"; break;
                    case 2: ans = "ผมชอบกินมาม่า อุ๊ป"; break;
                    case 3: ans = "กินข้าวด้วยน้าาา"; break;
                    case 4: ans = "เหงาใช่ไหมล่ะ"; break;
                    case 5: ans = "เหนื่อยไหมคนดี"; break;
                    case 6: ans = "ผมชื่อพอสนะฮะ"; break;
                    case 7: ans = "ว่างมากเลยอะดิ"; break;
                    case 8: ans = "คิดถึงกันใช่ไหม"; break;
                    case 9: ans = "พิม help จิเธอ"; break;
                    case 10: ans = "ข้าวมันไก่หลังม.อร่อยม๊ากกกกกกกก"; break;
                    case 11: ans = "SE3 2559 ทุกคนทั้งหล่อและสวย"; break;
                }
                replyMessage = new TextMessage(ans);


            }
            await Reply(replyMessage);
        }

        public async Task HandleMediaMessage()
        {
            Message message = JsonConvert.DeserializeObject<Message>(lineEvent.Message.ToString());
            // Get media from Line server.
            Media media = await lineClient.GetContent(message.Id);
            Message replyMessage = null;

            // Reply Image 
            switch (message.Type)
            {
                case MessageType.Image:
                case MessageType.Video:
                case MessageType.Audio:
                    replyMessage = new ImageMessage("https://github.com/apple-touch-icon.png", "https://github.com/apple-touch-icon.png");
                    break;
            }

            await Reply(replyMessage);
        }

        public async Task HandleStickerMessage()
        {
            //https://devdocs.line.me/files/sticker_list.pdf
            var stickerMessage = JsonConvert.DeserializeObject<StickerMessage>(lineEvent.Message.ToString());
            var replyMessage = new StickerMessage("1", "1");
            await Reply(replyMessage);
        }

        public async Task HandleLocationMessage()
        {
            var locationMessage = JsonConvert.DeserializeObject<LocationMessage>(lineEvent.Message.ToString());
            LocationMessage replyMessage = new LocationMessage(
                locationMessage.Title,
                locationMessage.Address,
                locationMessage.Latitude,
                locationMessage.Longitude);
            await Reply(replyMessage);
        }

        private async Task Reply(Message replyMessage)
        {

            try
            {
               
                await lineClient.ReplyToActivityAsync(lineEvent.CreateReply(message: replyMessage));
            }
            catch
            {
                await lineClient.PushAsync(lineEvent.CreatePush(message: replyMessage));
            }
        }
    }
}
