﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Cb.Model;
using Cb.Localization;
using Cb.BLL;
using System.IO;
using Cb.Model.Products;
using Cb.BLL.Product;
using Cb.DBUtility;
using System.Configuration;
using System.Web.UI.HtmlControls;
using Cb.Utility;
using Cb.Web.Common;

namespace Cb.Web.Controls
{
    public partial class top_menu : DGCUserControl
    {
        #region Parameter

        protected string pageName, template_path = string.Empty;

        private int id, total;

        IList<PNK_ProductCategory> lstAll;
        IList<PNK_ProductCategory> lstParent;
        private ProductCategoryBLL pcBll
        {
            get
            {
                if (ViewState["pcBll"] != null)
                    return (ProductCategoryBLL)ViewState["pcBll"];
                else return new ProductCategoryBLL();
            }
            set
            {
                ViewState["pcBll"] = value;
            }
        }

        #endregion

        #region Common

        private void InitPage()
        {
            GetMenu();
            GetLogo();

            ltrPhoneName.Text = LocalizationUtility.GetText("ltrPhoneName", Ci);
            ltrWokingTimeName.Text = LocalizationUtility.GetText("ltrWokingTimeName", Ci);
        }

        /// <summary>
        /// Get menu voi ParentID=0
        /// </summary>
        private void GetMenu()
        {
            //Lấy danh sách tổng bao gồm danh mục cha và cả danh mục con
            lstAll = pcBll.GetList(LangInt, string.Empty, "1", int.MinValue, true, "p.ordering", 1, 1000, out total);

            if (total > 0)
            {
                //Lấy danh sách danh mục cha có ParentID==0 gán vào menu cha         
                lstParent = lstAll.Where(m => m.ParentId == 0
                    && m.Id != DBConvert.ParseInt(ConfigurationManager.AppSettings["parentIdHome"])
                    ).ToList();
                if (lstParent.Count() > 0)
                {
                    rptResult.DataSource = lstParent;
                    rptResult.DataBind();
                }

            }
        }

        private void GetLogo()
        {
            ConfigurationBLL pcBll = new ConfigurationBLL();
            IList<PNK_Configuration> lst = pcBll.GetList();
            if (lst != null && lst.Count > 0)
            {
                foreach (PNK_Configuration item in lst)
                {
                    if (item.Key_name == Constant.Configuration.config_logoHeader)
                    {
                        imgLogo.Src = imgLogo.Src = WebUtils.GetUrlImage(Constant.DSC.AdvUploadFolder, item.Value_name);
                        hypIcon.HRef = WebUtils.RedirectHomePage();

                    }
                    if (item.Key_name == Constant.Configuration.config_fbfanpage)
                    {
                        hypFBFanpage.HRef = item.Value_name;
                    }
                    if (item.Key_name == Constant.Configuration.skypeid)
                    {
                        ltrWokingTimeValue.Text = item.Value_name;
                    }
                    if (item.Key_name == Constant.Configuration.email)
                    {
                        ltrEmail.Text = item.Value_name;
                    }
                    else if (item.Key_name == Constant.Configuration.config_twitter)
                    {
                        this.hypTwitter.HRef = item.Value_name;
                    }
                    else if (item.Key_name == Constant.Configuration.config_linkedIn)
                    {
                        this.hypLinkedIn.HRef = item.Value_name;
                    }

                    if (LangInt == 1)
                    {
                        if (item.Key_name == Constant.Configuration.config_address_vi)
                        {
                            ltrAddressValue.Text = item.Value_name;
                        }
                        if (item.Key_name == Constant.Configuration.phone)
                        {
                            ltrPhoneValue.Text = item.Value_name;
                        }

                    }
                    else
                    {
                        if (item.Key_name == Constant.Configuration.config_address1_vi)
                        {
                            ltrAddressValue.Text = item.Value_name;
                        }
                        if (item.Key_name == Constant.Configuration.phone)
                        {
                            ltrPhoneValue.Text = item.Value_name;
                        }
                    }
                }
            }
        }

        #endregion

        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitPage();
            }
        }

        protected void rptResult_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PNK_ProductCategory data = e.Item.DataItem as PNK_ProductCategory;

                HtmlAnchor hypName = e.Item.FindControl("hypName") as HtmlAnchor;
                hddParentNameUrl.Value = hypName.HRef = LinkHelper.GetLink(data.ProductCategoryDesc.NameUrl, LangId);

                Literal ltrName = e.Item.FindControl("ltrName") as Literal;
                hypName.Title = ltrName.Text = data.ProductCategoryDesc.Name;

                //Literal ltrIcon = e.Item.FindControl("ltrIcon") as Literal;
                //ltrIcon.Text = data.ImageFont;

                //Sub menu con              
                IList<PNK_ProductCategory> lstSub = lstAll.Where(m => m.ParentId == data.Id).ToList();
                if (lstSub.Count() > 0)
                {
                    //HtmlControl divIconSub = e.Item.FindControl("divIconSub") as HtmlControl;
                    //divIconSub.Attributes.Add("display", "block !important");

                    HtmlControl ulSub = e.Item.FindControl("ulSub") as HtmlControl;
                    ulSub.Visible = true;

                    hddParentNameUrl.Value = data.ProductCategoryDesc.NameUrl;

                    Repeater rptResultSub = e.Item.FindControl("rptResultSub") as Repeater;
                    rptResultSub.DataSource = lstSub;
                    rptResultSub.DataBind();
                }
            }
        }

        protected void rptResultSub_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //Repeater dataParent = (Repeater)e.Item.Parent.Parent;

                PNK_ProductCategory data = e.Item.DataItem as PNK_ProductCategory;
                string link = UtilityLocal.GetPathTreeNameUrl(data.Id, LangInt, LangId);
                HtmlAnchor hypNameSub2 = e.Item.FindControl("hypNameSub2") as HtmlAnchor;
                hypNameSub2.HRef = LinkHelper.GetLink(link);

                Literal ltrNameSub2 = e.Item.FindControl("ltrNameSub2") as Literal;
                hypNameSub2.Title = ltrNameSub2.Text = data.ProductCategoryDesc.Name;

                //HtmlImage img = e.Item.FindControl("img") as HtmlImage;
                //img.Src = WebUtils.GetUrlImage(ConfigurationManager.AppSettings["ProductCategoryUpload"], data.BaseImage);

                //Sub menu con              
                IList<PNK_ProductCategory> lstSub = lstAll.Where(m => m.ParentId == data.Id).ToList();
                if (lstSub.Count() > 0)
                {
                    hddParentNameUrl.Value = data.ProductCategoryDesc.NameUrl;

                    Repeater rptResultSub1 = e.Item.FindControl("rptResultSub1") as Repeater;
                    rptResultSub1.DataSource = lstSub;
                    rptResultSub1.DataBind();
                }
            }
        }

        protected void rptResultSub1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater dataParent = (Repeater)e.Item.Parent;

                PNK_ProductCategory data = e.Item.DataItem as PNK_ProductCategory;

                string link = UtilityLocal.GetPathTreeNameUrl(data.Id, LangInt, LangId);
                HtmlAnchor hypTitle = e.Item.FindControl("hypTitle") as HtmlAnchor;
                hypTitle.HRef = LinkHelper.GetLink(link);

                Literal ltrTitle = e.Item.FindControl("ltrTitle") as Literal;
                hypTitle.Title = ltrTitle.Text = data.ProductCategoryDesc.Name;
            }
        }

        #endregion
    }
}