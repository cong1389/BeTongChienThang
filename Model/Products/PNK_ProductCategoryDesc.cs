/**
                             * @version $Id:
                             * @package Digicom.NET
                             * @author Digicom Dev <dev@dgc.vn>
                             * @copyright Copyright (C) 2011 by Digicom. All rights reserved.
                             * @link http://www.dgc.vn
                            */

using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

namespace Cb.Model.Products
{
    public class PNK_ProductCategoryDesc
    {
        #region fields
        private int id;
        private int mainId;
        private int langId;
        private string name;
        private string nameUrl;
        private string brief;
        private string detail;

        private string metaTitle;
        private string metaKeyword;
        private string metaDecription;

        private string h1;
        private string h2;
        private string h3;

        private string treeNameDesc;
        #endregion

        #region properties
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public int MainId
        {
            get { return this.mainId; }
            set { this.mainId = value; }
        }
        public int LangId
        {
            get { return this.langId; }
            set { this.langId = value; }
        }
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public string NameUrl
        {
            get { return this.nameUrl; }
            set { this.nameUrl = value; }
        }
        public string Brief
        {
            get { return this.brief; }
            set { this.brief = value; }
        }
        public string Detail
        {
            get { return this.detail; }
            set { this.detail = value; }
        }

        public string MetaTitle
        {
            get { return this.metaTitle; }
            set { this.metaTitle = value; }
        }
        public string MetaKeyword
        {
            get { return this.metaKeyword; }
            set { this.metaKeyword = value; }
        }
        public string MetaDecription
        {
            get { return this.metaDecription; }
            set { this.metaDecription = value; }
        }

        public string H1
        {
            get { return this.h1; }
            set { this.h1 = value; }
        }

        public string H2
        {
            get { return this.h2; }
            set { this.h2 = value; }
        }

        public string H3
        {
            get { return this.h3; }
            set { this.h3 = value; }
        }

        public string TreeNameDesc
        {
            get { return this.treeNameDesc; }
            set { this.treeNameDesc = value; }
        }

        #endregion

        #region constructor
        public PNK_ProductCategoryDesc()
        {
            this.id = int.MinValue;
            this.mainId = int.MinValue;
            this.langId = int.MinValue;
            this.name = string.Empty;
            this.nameUrl = string.Empty;
            this.brief = string.Empty;
            this.detail = string.Empty;
            this.metaTitle = string.Empty;
            this.metaKeyword = string.Empty;
            this.metaDecription = string.Empty;
            this.h1 = string.Empty;
            this.h2 = string.Empty;
            this.h3 = string.Empty;

            this.treeNameDesc = string.Empty;
        }
        public PNK_ProductCategoryDesc(int id,
                    int mainId,
                    int langId,
                    string name,
                    string nameUrl,
                    string brief,
                    string detail,
                    string metaTitle,
                    string metaKeyword,
                    string metaDecription,
                    string h1,
                    string h2,
                    string h3,
                    string treeNameDesc)
        {
            this.id = id;
            this.mainId = mainId;
            this.langId = langId;
            this.name = name;
            this.nameUrl = nameUrl;
            this.brief = brief;
            this.detail = detail;
            this.metaTitle = metaTitle;
            this.metaKeyword = metaKeyword;
            this.metaDecription = metaDecription;
            this.h1 = h1;
            this.h2 = h2;
            this.h3 = h3;
            this.treeNameDesc = treeNameDesc;
        }
        #endregion

    }
}