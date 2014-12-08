using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xceed.Data.Mongodb;
using Xceed.Model.Entities;

namespace UserInterfaceMvc.Controllers
{
    public class HomeController : Controller
    {
        // instance variable or property
        private Mongodb database;
        public HomeController()
        {
            database = new Mongodb();
        }
        public ActionResult Index()
        {
            // delete all old tweets
            database.dropDocumnet(); 
            // generate new tweets 
            database.SaveJsonFile();
            return View();
        }


        public ActionResult Tweets_100()
        { 
         return View(database.Get100Tweets());
        }
        public ActionResult Top5retweet()
        {
            return View(getTop5Retweet());
        }

        public ActionResult Top5followed()
        {

            return View(getTop5followed());
        }

 
        /*
         * GgetAll100Tweets() return a list of 100 tweets.
         * 
         */
        public List<ModelTweet> getAll100Tweets()
        {
            return database.Get100Tweets();
        }


        /*
         *getTop5followed() return top 5 followed.
         */

        public List<ModelTweet> getTop5followed()
        {
            return database.getTop5followed();
        }


        /*
        *getTop5Retweet() return top 5 retweet.
        */

        public List<ModelTweet> getTop5Retweet()
        {
            return database.getTop5Retweet();
        }

    }
}