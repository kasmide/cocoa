﻿using System;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;

// Aliases Func<AppQuery, AppQuery> with Query
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

//(陽性情報の登録画面)
namespace CovidRadar.UITestV2
{
    public class NotifyOtherPage : BasePage
    {
        /***********
         * 陽性情報の登録
        ***********/

        readonly Query openHowToReceiveProcessingNumberBtn_NotCheckRadioBtn;
        readonly Query openHowToReceiveProcessingNumberBtn_CheckedRadioBtn;
        readonly Query SymptomRadioBtn;
        readonly Query ProcessingNumberForm;
        readonly Query RegisterBtn;
        readonly Query RegisterConfirmBtn;
        readonly Query toolBarBack;
        readonly Query RegisterCancelBtn;
        readonly Query CancelDialogOKBtn;

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = x => x.Marked("NotifyOtherPageTitle"),
            iOS = x => x.Marked("NotifyOtherPageTitle")
        };

        public NotifyOtherPage()
        {
            


            //CancelDialogOKBtn = x => x.Id("message");//「登録をキャンセルしました」ダイアログでのOKボタン


            if (OnAndroid)
            {
                openHowToReceiveProcessingNumberBtn_NotCheckRadioBtn = x => x.Marked("NotifyOtherPageTitle").Class("LabelRenderer").Index(3);//処理番号の取得方法(ラジオボタン未選択時)
                openHowToReceiveProcessingNumberBtn_CheckedRadioBtn = x => x.Marked("NotifyOtherPageTitle").Class("LabelRenderer").Index(4);//処理番号の取得方法(ラジオボタン選択時)
                SymptomRadioBtn = x => x.Marked("NotifyOtherPageTitle").Class("RadioButtonRenderer").Index(0);//症状の有無(あり)ラジオボタン
                ProcessingNumberForm = x => x.Marked("NotifyOtherPageTitle").Class("MaterialFormsTextInputLayout").Index(0);//陽性番号入力フォーム
                RegisterBtn = x => x.Marked("NotifyOtherPageTitle").Class("ButtonRenderer").Index(0);//登録するボタン
                RegisterConfirmBtn = x => x.Id("button1");//2種類のボタンに同じIDが振られていることに注意。陽性情報の登録をしますダイアログ→(「登録」ボタン)　　COVID-19接触のログ記録を有効にしてください→(「OK」ボタン)
                RegisterCancelBtn = x => x.Id("button2");//陽性情報の登録をしますダイアログ→(「キャンセル」ボタン)
                CancelDialogOKBtn = x => x.Id("button1");//「登録をキャンセルしました」ダイアログでのOKボタン
                toolBarBack = x => x.Id("toolbar").Class("AppCompatImageButton").Index(0); //戻るボタン
            }

            if (OniOS)
            {
                openHowToReceiveProcessingNumberBtn_NotCheckRadioBtn = x => x.Marked("NotifyOtherPageTitle").Class("UILabel").Index(5);//処理番号の取得方法(ラジオボタン未選択時)
                openHowToReceiveProcessingNumberBtn_CheckedRadioBtn = x => x.Marked("NotifyOtherPageTitle").Class("UILabel").Index(6);//処理番号の取得方法(ラジオボタン選択時)
                SymptomRadioBtn = x => x.Marked("NotifyOtherPageTitle").Class("UILabel").Index(2);//症状の有無(あり)ラジオボタン
                ProcessingNumberForm = x => x.Id("NotifyOtherPageTitleEntry");//陽性番号入力フォーム
                RegisterBtn = x => x.Marked("NotifyOtherPageTitle").Class("UIButton").Index(0);//登録するボタン
                toolBarBack = x => x.Class("UIButton").Index(1); //戻るボタン
            }
        }

        // ページ表示確認
        public void AssertNotifyOtherPage(TimeSpan? timeout = default(TimeSpan?))
        {
            app.Screenshot(this.GetType().Name.ToString());
            base.AssertOnPage(timeout);
        }

        public void ToolBarBack()
        {
            app.Tap(toolBarBack);
        }

        public HowToReceiveProcessingNumberPage OpenHowToReceiveProcessingNumber(string radioBtn="")
        {
            if (radioBtn == "checked")
            {
                app.Tap(openHowToReceiveProcessingNumberBtn_CheckedRadioBtn);
            }
            else {
                app.Tap(openHowToReceiveProcessingNumberBtn_NotCheckRadioBtn);
            }
            return new HowToReceiveProcessingNumberPage();
        }


        public void TapYesRadioBtn()
        {
            app.Tap(SymptomRadioBtn);
        }

        public void EnterProcessingNumberForm(string processingNumber="00000000")
        {

            if (OnAndroid)
            {
                app.ScrollDownTo(ProcessingNumberForm);
            }

            if (OniOS)
            {
                app.ScrollDownTo("NotifyOtherPageTitleEntry", "NotifyOtherPageTitleScrollView");
            }
            app.Tap(ProcessingNumberForm);
            app.ClearText(ProcessingNumberForm);
            app.EnterText(processingNumber);
            app.DismissKeyboard();
        }

        public void TapRegisterBtn()
        {
            if (OnAndroid)
            {
                app.ScrollDownTo(RegisterBtn);
            }

            if (OniOS)
            {
                app.ScrollDownTo("SubmitConsentPageBtn", "NotifyOtherPageTitleScrollView");
            }
            app.Tap(RegisterBtn);
        }
        public void TapRegisterConfirmBtn(String cultureText = "")
        {

            if (OnAndroid)
            {
                app.Tap(RegisterConfirmBtn);//陽性情報の登録をしますダイアログ→(「登録」ボタン)
            }

            if (OniOS)
            {
                string ComparisonText = (string)AppManager.Comparison(cultureText, "ButtonRegister");
                app.Tap(ComparisonText);//陽性情報の登録をしますダイアログ→(「登録」ボタン)
            }

        }
        public void TapRegisterCancelBtn(String cultureText = "")
        {

            if (OnAndroid)
            {
                app.Tap(CancelDialogOKBtn);
            }

            if (OniOS)
            {
                string ComparisonText = (string)AppManager.Comparison(cultureText, "ButtonCancel");
                app.Tap(ComparisonText);//陽性情報の登録をしますダイアログ→(「登録」ボタン)
            }
        }
        public void TapCancelDialogOKBtn()
        {
            if (OnAndroid)
            {
                app.Tap(CancelDialogOKBtn);
            }

            if (OniOS)
            {
                app.Tap("OK");//陽性情報の登録をしますダイアログ→(「登録」ボタン)
            }
        }
    }

}
