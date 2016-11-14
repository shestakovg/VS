using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using System.Web.Script.Serialization;
using ActiveUp.Net.Mail;
using TradeServices.DataEntitys.IncomingData;

namespace TradeServices.Classes
{
    enum  MailMessageType
    {
        location,
        checkin
    }

    public class MailLocation: IDisposable
    {
        private CancellationTokenSource source = null;
        private CancellationToken token;
        private ProcessOrderLog log = null;
        private static MailLocation mailLocation=null;
        //private MailRepository mailRepository;

        private String gmailUser;
        private String gmailPassword;
        protected MailLocation(String gmailUser, String gmailPassword)
        {
            this.log = new ProcessOrderLog(SaveData.getConnection());
            log.WriteLog(Guid.NewGuid(), "MailLocation was created");
            this.source = new CancellationTokenSource();
            this.token = source.Token;
            this.gmailUser = gmailUser;
            this.gmailPassword = gmailPassword;
            
            // this.Start();
        }

        public static MailLocation GetInstance(String gmailUser, String gmailPassword)
        {
            if (mailLocation == null)
            {
                MailLocation.mailLocation = new MailLocation(gmailUser, gmailPassword);
            }
            return MailLocation.mailLocation;
        }

        public static bool IsInstance()
        {
            return !(mailLocation == null);
        }

        public static MailLocation GetInstance()
        {
            if (mailLocation == null)
            {
                throw new ArgumentNullException("MailLocation doesn't inialized");
            }
            return MailLocation.mailLocation;
        }

        public void Start()
        {
            
            Task.Factory.StartNew(() => this.doBackgroundTask());
            log.WriteLog(Guid.NewGuid(), "MailLocation has been started");
        }

        public void Stop()
        {
            
            this.source.Cancel();
            log.WriteLog(Guid.NewGuid(), "MailLocation has been Stopped");
        }

        private void doBackgroundTask()
        {
            try
            {
                while (true)
                {
                    if (this.token.IsCancellationRequested)
                    {
                        break;
                    }

                    //log.WriteLog(Guid.NewGuid(), "doBackgroundTask started");
                    var mailRepository = new MailRepository(
                          "imap.gmail.com",
                          993,
                          true,
                         this.gmailUser,
                          this.gmailPassword
                      );
                    //log.WriteLog(Guid.NewGuid(), "MailRepository created successfuly");

                    var emailList = mailRepository.GetAllMails("inbox");

                    
                    log.WriteLog(Guid.NewGuid(), "Has got "+ (emailList != null? emailList.Count().ToString() : "null"));

                    foreach (Message email in emailList)
                    {
                       if (email.Subject.Equals(MailMessageType.location.ToString()))
                       {
                            try
                            {
                                JavaScriptSerializer j = new JavaScriptSerializer();
                                //String jsonString = email.BodyText.Text.Replace("\"checkInArray\":", "");
                                TrackingEntity[]trackingAray = j.Deserialize<TrackingEntity[]>(email.BodyText.Text);
                                TradeServices.Dictionary dic = new Dictionary();
                                dic.SaveTracking(trackingAray);
                                log.WriteLog(Guid.NewGuid(), "location has been processed "+email.Date.ToString());
                            }
                            catch (Exception e)
                            {
                                log.WriteLog(Guid.NewGuid(), "Process location error: " + e.Message);
                            }
                        }
                        else if (email.Subject.Equals(MailMessageType.checkin.ToString()))
                       {
                           try
                           {
                                JavaScriptSerializer j = new JavaScriptSerializer();
                                //String jsonString = email.BodyText.Text.Replace("\"checkInArray\":", "");
                                OutletCheckIn[] checkInAray = j.Deserialize<OutletCheckIn[]>(email.BodyText.Text);
                                TradeServices.Dictionary dic = new Dictionary();
                                dic.SaveCheckIn(checkInAray);
                                log.WriteLog(Guid.NewGuid(), "checkin has been processed " + email.Date.ToString());
                            }
                           catch (Exception e)
                           {
                                log.WriteLog(Guid.NewGuid(), "Process checkin error: " + e.Message);
                            }
                           
                        }
                    }
                    mailRepository.DeleteMessages(emailList, "inbox");
                    mailRepository = null;
                    Thread.Sleep(60 * 1000 * 3);
                }
            }
            catch (Exception e)
            {

                log.WriteLog(Guid.NewGuid(), "Get gmail error: "+e.Message);
            }
        }

        public void Dispose()
        {
            
            this.Stop();
        }
    }
}