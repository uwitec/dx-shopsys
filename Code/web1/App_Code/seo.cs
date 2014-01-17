using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace web1
{
    public sealed class seo
    {
        string title_, key_, desc_;
        public string title
        {
            get { return title_; }
            set { title_ = value; }
        }
        public string key
        {
            get { return key_; }
            set { key_ = value; }
        }
        public string desc
        {
            get { return desc_; }
            set { desc_ = value; }
        }
        
    }
}
