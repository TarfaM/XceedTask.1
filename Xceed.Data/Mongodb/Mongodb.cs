/* 
 * Author:Tarfa Mansour
 * Date :starting on 29-11-2014 
 * the purpose  of this class is create a active connection to localhost server of mongodb 
 * -get all (Top 5 retweets, Top 5 followed and 100 tweets.
 * - drop documnets of current collection.
 */

using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Data.JSON;
using Xceed.Data.TwitterApi.v1._1.REST_API;
using Xceed.Model.Entities;

namespace Xceed.Data.Mongodb
{

    public class Mongodb
    {
        private MongoDatabase database;
        private GET_home_timeline get_home_timeline;
        private string collctionName ;
        private string DatabaseName;
        //constructor
    public Mongodb()
    {
        get_home_timeline = new GET_home_timeline();
        // set a collection name
        collctionName = "feed";
        // set a database name
        DatabaseName = "storeData";
    }

    /*
     * Getmongodb(ModelTweet modleTeet) method passing the ModelTweet object parameter
     * the goal is connect ,choose db ,choose collection then finally insert data into mongodb
     */
    public void Getmongodb(ModelTweet modleTeet)
    {
        // localhost name for mongodb
        MongoClient client = new MongoClient("mongodb://localhost:27017");
        // create server for this machine
        var server = client.GetServer();
        //choose required db
        this.database = server.GetDatabase(DatabaseName);
        MongoCollection collection = database.GetCollection<ModelTweet>(collctionName);
        collection.Insert(modleTeet);
    }



    /*
     * GetmongodbConnected() method without parameter, it is for connect  and choose db 
     */
    public void GetmongodbConnected()
    {
        try {
            MongoClient client = new MongoClient();
            var server = client.GetServer();
            this.database = server.GetDatabase(DatabaseName);
        
        } 
      catch(MongoConnectionException ex)
        {
            Console.WriteLine(ex.Message);
        
        }

    }


    /*
     * getAllTweets()  it holding all data from mongo db then put it into ModelTweet object
     */
    public MongoCollection<ModelTweet> getAllTweets()
    {
         GetmongodbConnected();
         MongoCollection<ModelTweet> feedscollection = this.database.GetCollection<ModelTweet>(collctionName);
         return feedscollection;  
    }

    /*
    * getTop5followed()  return Top 5 followed from mongo db
    */

    public List<ModelTweet> getTop5followed()
    {
           try
          {
            MongoCollection<ModelTweet> collection = getAllTweets();
            return collection.FindAll().ToList<ModelTweet>().OrderByDescending(n => n.userFollowers).Distinct().Take(5).Distinct().ToList(); //top follwed
        }
        catch (MongoConnectionException)
        {
            return new List<ModelTweet>();

        }

    }
    /*
    * getTop5Retweet()  return Top 5 retweets from mongo db
    */
    public List<ModelTweet> getTop5Retweet()
    {
           try
        {

            MongoCollection<ModelTweet> collection = getAllTweets();
            return collection.FindAll().ToList<ModelTweet>().Distinct().OrderByDescending(n => n.retweetCount).Take(5).ToList(); //Top retweet
       }
        catch (MongoConnectionException)
        {
            return new List<ModelTweet>();
        }

    }
    /*
    * getTop5Retweet()  return 100 tweets from mongo db
    */
    public List<ModelTweet> Get100Tweets()
    {
       try
        {
            MongoCollection<ModelTweet> collection = getAllTweets();
            return collection.FindAll().ToList<ModelTweet>().Distinct().Take(100).ToList(); 
        }
        catch (MongoConnectionException)
        {
            return new List<ModelTweet>();

        }

    }


    /*
     * SaveJsonFile()  first deserialize  a JSON file into JSONTweet object, then take requird values from JSONTweet 
     * into  ModelTweet object by using foreach.
     * it retruns a list of tweets  
    */
    public List<ModelTweet> SaveJsonFile()
    {
        // deserialize  a JSON file into JSONTweet object
        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JSONTweet>>(get_home_timeline.getTimeLineTweets());
        // create ModelTweet object to hold required values
        ModelTweet feed = new ModelTweet();
        //create list of ModelTweet object
        List<ModelTweet> listFeed = new List<ModelTweet>();
                foreach (var c in result)
        {
            // reset Model object to recive next values
            feed.clear();
            //add a tweet text
            feed.Text = c.text.ToString();
            //add a date of tweet
            feed.dateText = c.created_at;
            //add account name
            feed.userName = c.user.name;
            //add follwer number       
            feed.userFollowers = c.user.followers_count;
            //add url of image 
            feed.userImageProfile = c.user.profile_image_url;
            // add retweet number 
            feed.retweetCount = c.retweet_count;
            // add ModelTweet into list of ModelTweet object
            listFeed.Add(feed);
            // save into database 
            this.Getmongodb(feed); 
        }
        return listFeed.ToList();

    }


    /*
     * dropDocumnet() drop documnets of current collection
     */
    public void dropDocumnet() 
    {
        getAllTweets().Drop();
    }
    }
}
