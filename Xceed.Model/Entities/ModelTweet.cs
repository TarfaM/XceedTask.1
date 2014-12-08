/*
 * Author:Tarfa Mansour 
 * Date :starting on 29-11-2014 
 * the purpose  of this class is create a class is holding all required properties  
 * this class will used later to store data into mongodb throw list of objects from this class.
 */

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xceed.Model.Entities
{
  public class ModelTweet
    {
        //constrouctor 
            public   ModelTweet(){}

    /*
     * clear() method is setting all properties' value for defuault value.
     */
    public void clear()
            {
                this.Id = Guid.Empty;
                this.Text = "";
                this.dateText = "";
                this.userName = "";
                this.userFollowers = 0;
                this.userImageProfile = "";
                this.retweetCount = 0;
            }

    /*
    * All required properties that will stored in to mongo db 
    */
    [BsonId(IdGenerator = typeof(CombGuidGenerator))]
    public Guid Id { get; set; }
    [BsonElement("Text")]
    public string Text { get; set; }
    [BsonElement("dateText")]
    public string dateText { get; set; }
    [BsonElement("userName")]
    public string userName { get; set; }
    [BsonElement("userFollowers")]
    public int userFollowers { get; set; }
    [BsonElement("userImageProfile")]
    public String userImageProfile { get; set; }
    [BsonElement("retweetCount")]
    public int retweetCount { get; set; }



    }
}
