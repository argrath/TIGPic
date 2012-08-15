using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Text.RegularExpressions;
using System.Xml;

namespace WindowsFormsApplication1 {
    public class Parse {
        protected XmlDocument d;
        protected XmlNamespaceManager n;

        public string ImageURL {
            get;
            set;
        }

        public Parse(string URL) {
            string extracted = Extract(URL);
            foreach(ParseBase pb in ParseBase.SiteList){
                if(pb.Check(extracted)){
                    ImageURL = pb.Parse(extracted);
                    break;
                }
            }
        }

        private string Extract(string str) {
            Match m = Regex.Match(str, "http[^ \"]*");
            return m.Groups[0].Value;
        }

        private void ParseTwitPic(string URL) {
            WebClient wc = new WebClient();
            string content = wc.DownloadString(URL);

            Match m = Regex.Match(content, "<meta name=\"twitter:image\" value=\"(?<1>.*)\" />");
            ImageURL = m.Groups[1].Value;
        }

        private void ParseInstagram(string URL) {
            WebClient wc = new WebClient();
            string content = wc.DownloadString(URL);

            Match m = Regex.Match(content, "<img src=\"(?<1>.*)\" class=\"photo\" />");
            ImageURL = m.Groups[1].Value;
        }

    }

    public class ParseBase {
        public static List<ParseBase> SiteList = new List<ParseBase>();

        private string ID;
        private string Pattern;

        public ParseBase(string id, string pattern) {
            ID = id;
            Pattern = pattern;
            SiteList.Add(this);
        }

        public bool Check(string URL) {
            return Regex.Match(URL, ID).Success;
        }

        public string Parse(string URL) {
            WebClient wc = new WebClient();
            string content = wc.DownloadString(URL);

            Match m = Regex.Match(content, Pattern);
            return m.Groups[1].Value;
        }

        public static readonly ParseBase TwitPic = new ParseBase(
            "http://twitpic",
            "<meta name=\"twitter:image\" value=\"(?<1>.*)\" />"
            );
        public static readonly ParseBase Instagram = new ParseBase(
            "http://instagr.am",
            "<img class=\"photo\" src=\"(?<1>.*)\" />"
            );
        public static readonly ParseBase MovaPic = new ParseBase(
            "http://movapic.com/",
            "(?<1>http://image.movapic.com/pic/m_[^\"]*)\""
            );
        public static readonly ParseBase Twipple = new ParseBase(
            "http://p.twipple.jp",
            "(?<1>http://p.twpl.jp/show/orig/[^\"]*)\""
            );
    }
}
