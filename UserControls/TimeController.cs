using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DTBGEmulator.UserControls
{
    public partial class TimeController : UserControl
    {
        #region 변수 정의
        // 전체 시뮬레이션 시간 [sec]
        private string mTotalTime = string.Empty;
        public string TotalTime
        {
            set
            {
                mTotalTime = value; DoInitialize();

                panel_Middle.Invalidate();
            }
        }

        // 현재 시간 [sec]. 즉, Mainbar에서 Circle의 위치를 시간으로 치환한 값
        private string mCurrTime;
        public string CurrTime
        {
            get
            {
                return (mTotalTime == string.Empty ? "0" : Math.Round(Convert.ToInt32(mTotalTime) * mProgress, 0).ToString());
            }
            set
            {
                if (mTotalTime != string.Empty)
                {
                    if (Convert.ToInt32(value) < mStartTime)
                    {
                        mCurrTime = mStartTime.ToString();
                    }
                    else if (Convert.ToInt32(value) > mEndTime)
                    {
                        mCurrTime = mEndTime.ToString();
                    }
                    else
                    {
                        mCurrTime = value;
                    }

                    mProgress = Convert.ToInt32(mCurrTime) / (float)(Convert.ToInt32(mTotalTime));

                    panel_Middle.Invalidate();
                }
            }
        }

        // Start Selector의 위치를 시간으로 치환한 값 [sec]
        private int mStartTime;
        public string StartTime
        {
            get
            {
                return mStartTime.ToString();
            }
        }

        // End Selector의 위치를 시간으로 치환한 값 [sec]
        private int mEndTime;
        public string EndTime
        {
            get
            {
                return mEndTime.ToString();
            }
        }

        // 각종 크기 및 초기 설정
        private float mHorizontalMargin = 9.0f;
        private float mTopMargin = 18.0f;
        private float mBarThickness = 2.0f;
        private float mProgress = 0.0f; // Progress Rate [0.0 ~ 1.0]
        private float mRadius = 7.0f;
        private float mSelectorL = 13.0f;
        private int mSelectorGap = 15;
        private int mSelectorOffset = 2;

        // Mainbar의 Circle 관련 변수
        private RectangleF mRectCircle;
        private bool mIsCircleClicking = false;

        // Start Time Selector 관련 변수
        private PointF[] mPointsStartTimeSelector = new PointF[3];
        private Region mRegionStartTimeSelector;
        private bool mIsStartTimeSelectorClicking = false;
        private float mStartTimeRatio;

        // End Time Selector 관련 변수
        private PointF[] mPointsEndTimeSelector = new PointF[3];
        private Region mRegionEndTimeSelector;
        private bool mIsEndTimeSelectorClicking = false;
        private float mEndTimeRatio;
        #endregion 변수 정의

        public TimeController()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(64, 64, 64);  // UC 백그라운드 색상 변경

            // Start/End TimeSelector 초기위치 설정
            mPointsStartTimeSelector[0] = new PointF(mHorizontalMargin, mTopMargin - mSelectorOffset);
            mPointsEndTimeSelector[0] = new PointF(panel_Middle.Width - mHorizontalMargin, mTopMargin - mSelectorOffset);
        }

        private void DoInitialize()
        {
            mPointsStartTimeSelector[0] = new PointF(mHorizontalMargin, mTopMargin - mSelectorOffset);
            mPointsEndTimeSelector[0] = new PointF(panel_Middle.Width - mHorizontalMargin, mTopMargin - mSelectorOffset);
            mStartTime = 0;
            mEndTime = Convert.ToInt32(mTotalTime);
            mProgress = 0.0f;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Time[sec]를 Start/End Time 텍스트 박스에 '00 : 00 : 00' 형태로 입력하는 함수

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // MainBar 업데이트
        private void UpdateMainBar(Graphics graphics)
        {
            if (mTotalTime != string.Empty)

            mRectCircle = new RectangleF(mHorizontalMargin + (panel_Middle.Width - 2 * mHorizontalMargin) * mProgress - mRadius, mTopMargin + mBarThickness / 2.0f - mRadius, 2 * mRadius, 2 * mRadius);

            float foreBarLength = mRectCircle.Left + mRadius - mHorizontalMargin;
            float aftBarLength = mPointsEndTimeSelector[0].X - mHorizontalMargin - foreBarLength;
            float backBarLength = (panel_Middle.Width - 2 * mHorizontalMargin) - foreBarLength - aftBarLength;

            RectangleF rectFore = new RectangleF(mHorizontalMargin, mTopMargin, foreBarLength, mBarThickness);
            RectangleF rectAft = new RectangleF(mHorizontalMargin + foreBarLength, mTopMargin, aftBarLength, mBarThickness);
            RectangleF rectBackBar = new RectangleF(mHorizontalMargin + foreBarLength + aftBarLength, mTopMargin, backBarLength, mBarThickness);

            using (SolidBrush brushForeBar = new SolidBrush(Color.FromArgb(146, 208, 80)))
            using (SolidBrush brushAftBar = new SolidBrush(Color.White))
            using (SolidBrush brushBackBar = new SolidBrush(Color.FromArgb(127, 127, 127)))
            {
                graphics.FillRectangle(brushForeBar, rectFore);
                graphics.FillRectangle(brushAftBar, rectAft);
                graphics.FillRectangle(brushBackBar, rectBackBar);

                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.FillEllipse(brushForeBar, mRectCircle);
                graphics.SmoothingMode = SmoothingMode.Default;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // UpdateStartTimeSelector 업데이트
        private void UpdateStartTimeSelector(Graphics graphics)
        {
            mPointsStartTimeSelector[1] = new PointF((float)(mPointsStartTimeSelector[0].X - mSelectorL * Math.Cos((Math.PI / 180) * 60)), (float)(mPointsStartTimeSelector[0].Y - mSelectorL * Math.Sin((Math.PI / 180) * 60)));
            mPointsStartTimeSelector[2] = new PointF((float)(mPointsStartTimeSelector[0].X + mSelectorL * Math.Cos((Math.PI / 180) * 60)), (float)(mPointsStartTimeSelector[0].Y - mSelectorL * Math.Sin((Math.PI / 180) * 60)));

            GraphicsPath path = new GraphicsPath();
            path.AddLine(mPointsStartTimeSelector[0], mPointsStartTimeSelector[1]);
            path.AddLine(mPointsStartTimeSelector[1], mPointsStartTimeSelector[2]);
            path.AddLine(mPointsStartTimeSelector[2], mPointsStartTimeSelector[0]);

            mRegionStartTimeSelector = new Region(path);

            using (SolidBrush brushSelector = new SolidBrush(Color.White))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.FillPolygon(brushSelector, mPointsStartTimeSelector);
                graphics.SmoothingMode = SmoothingMode.Default;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // UpdateEndTimeSelector 업데이트
        private void UpdateEndTimeSelector(Graphics graphics)
        {
            mPointsEndTimeSelector[1] = new PointF((float)(mPointsEndTimeSelector[0].X - mSelectorL * Math.Cos((Math.PI / 180) * 60)), (float)(mPointsEndTimeSelector[0].Y - mSelectorL * Math.Sin((Math.PI / 180) * 60)));
            mPointsEndTimeSelector[2] = new PointF((float)(mPointsEndTimeSelector[0].X + mSelectorL * Math.Cos((Math.PI / 180) * 60)), (float)(mPointsEndTimeSelector[0].Y - mSelectorL * Math.Sin((Math.PI / 180) * 60)));

            GraphicsPath path = new GraphicsPath();
            path.AddLine(mPointsEndTimeSelector[0], mPointsEndTimeSelector[1]);
            path.AddLine(mPointsEndTimeSelector[1], mPointsEndTimeSelector[2]);
            path.AddLine(mPointsEndTimeSelector[2], mPointsEndTimeSelector[0]);

            mRegionEndTimeSelector = new Region(path);

            using (SolidBrush brushSelector = new SolidBrush(Color.White))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.FillPolygon(brushSelector, mPointsEndTimeSelector);
                graphics.SmoothingMode = SmoothingMode.Default;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // String 타입의 Seconds를 String 타입의 시간형태(HH : mm : ss)로 변환하는 함수
        private string ConvertSecToTime(string strSec)
        {
            int intSec = Convert.ToInt32(strSec);

            string HH = string.Format("{0:D2}", intSec / 3600);
            string mm = string.Format("{0:D2}", intSec % 3600 / 60);
            string ss = string.Format("{0:D2}", intSec % 60);

            string strTime = HH + " : " + mm + " : " + ss;
            return strTime;
        }

        #region 이벤트 함수
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 폼 로드시 처리


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // panel_Middle 다시 그려져야 하는 이벤트 발생시 처리
        private void panel_Middle_Paint(object sender, PaintEventArgs e)
        {
            UpdateMainBar(e.Graphics);
            UpdateStartTimeSelector(e.Graphics);
            UpdateEndTimeSelector(e.Graphics);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // UC_Base1 사이즈 변경시 처리
        private void UC_TimeController_SizeChanged(object sender, EventArgs e)
        {
            // panel_Middle 폭 계산
            panel_Middle.Width = this.ClientRectangle.Width - 2 * 110;

            // UserControl 폼 높이 고정
            if (this.ClientRectangle.Height != 44)
            {
                this.Height = 44;
            }

            // UserControl 폼 폭 고정
            if (this.ClientRectangle.Width < 360)
            {
                this.Width = 360;
            }

            // End Time Selector 위치 조정
            mPointsEndTimeSelector[0] = new PointF(panel_Middle.Width - mHorizontalMargin, mTopMargin - mSelectorOffset);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // panel_Middle 마우스 다운 이벤트 발생시 처리
        private void panel_Middle_MouseDown(object sender, MouseEventArgs e)
        {
            // Mainbar - StartTime Selector 마우스 다운시 처리 ===================================================================
            if (mRegionStartTimeSelector.IsVisible(new PointF(e.X, e.Y)))
            {
                mIsStartTimeSelectorClicking = true;
            }

            // Mainbar - EndTime Selector 마우스 다운시 처리 =====================================================================
            if (mRegionEndTimeSelector.IsVisible(new PointF(e.X, e.Y)))
            {
                mIsEndTimeSelectorClicking = true;
            }

            // Mainbar - Circle 마우스 다운시 처리 ===============================================================================
            if (mRectCircle.Contains(new PointF(e.X, e.Y)) && !mIsStartTimeSelectorClicking && !mIsEndTimeSelectorClicking)
            {
                mIsCircleClicking = true;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // panel_Middle 마우스 이동 이벤트 발생시 처리
        private void panel_Middle_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // Circle 클릭상태로 마우스 이동시 처리 ===========================================================================
                if (mIsCircleClicking)
                {
                    if (e.X < mPointsStartTimeSelector[0].X)
                    {
                        mProgress = (mPointsStartTimeSelector[0].X - mHorizontalMargin) / (panel_Middle.Width - 2 * mHorizontalMargin);
                    }
                    else if (e.X > mPointsEndTimeSelector[0].X)
                    {
                        mProgress = (mPointsEndTimeSelector[0].X - mHorizontalMargin) / (panel_Middle.Width - 2 * mHorizontalMargin);
                    }
                    else
                    {
                        mProgress = (e.X - mHorizontalMargin) / (panel_Middle.Width - 2 * mHorizontalMargin);
                    }

                    panel_Middle.Invalidate();
                }

                // StartTime Selector 클릭상태로 마우스 이동시 처리 ===============================================================
                if (mIsStartTimeSelectorClicking)
                {
                    // X 좌표 제한
                    if (e.X > mPointsEndTimeSelector[0].X - mSelectorGap)
                    {
                        mPointsStartTimeSelector[0].X = mPointsEndTimeSelector[0].X - mSelectorGap;
                    }
                    else if (e.X < mHorizontalMargin)
                    {
                        mPointsStartTimeSelector[0].X = mHorizontalMargin;
                    }
                    else
                    {
                        mPointsStartTimeSelector[0].X = e.X;
                    }

                    // StartTimeSelector의 X좌표로부터 mStartTime 계산
                    if (mTotalTime != string.Empty)
                        mStartTime = (int)Math.Round((mPointsStartTimeSelector[0].X - mHorizontalMargin) / (panel_Middle.Width - 2 * mHorizontalMargin) * Convert.ToInt32(mTotalTime), 0);


                    // Circle 앞에서 뒤로 밀고가기
                    if (e.X > mRectCircle.Left + mRadius)
                    {
                        if (e.X > mPointsEndTimeSelector[0].X - mSelectorGap)
                        {
                            if (!mRectCircle.Contains(mPointsEndTimeSelector[0]))
                                mProgress = (mPointsEndTimeSelector[0].X - mSelectorGap - mHorizontalMargin) / (panel_Middle.Width - 2 * mHorizontalMargin);
                        }
                        else
                        {
                            mProgress = (e.X - mHorizontalMargin) / (panel_Middle.Width - 2 * mHorizontalMargin);
                        }
                    }

                    panel_Middle.Invalidate();
                }

                // EndTime Selector 클릭상태로 마우스 이동시 처리 =================================================================
                if (mIsEndTimeSelectorClicking)
                {
                    // X 좌표 제한
                    if (e.X < mPointsStartTimeSelector[0].X + mSelectorGap)
                    {
                        mPointsEndTimeSelector[0].X = mPointsStartTimeSelector[0].X + mSelectorGap;
                    }
                    else if (e.X > panel_Middle.Width - mHorizontalMargin)
                    {
                        mPointsEndTimeSelector[0].X = panel_Middle.Width - mHorizontalMargin; ;
                    }
                    else
                    {
                        mPointsEndTimeSelector[0].X = e.X;
                    }

                    // EndTimeSelector의 X좌표로부터 mEndTime 계산
                    if (mTotalTime != string.Empty)
                        mEndTime = (int)Math.Round((mPointsEndTimeSelector[0].X - mHorizontalMargin) / (panel_Middle.Width - 2 * mHorizontalMargin) * Convert.ToInt32(mTotalTime), 0);


                    // Circle 뒤에서 앞으로 밀고가기
                    if (e.X < mRectCircle.Right - mRadius)
                    {
                        if (e.X < mPointsStartTimeSelector[0].X + mSelectorGap)
                        {
                            if (!mRectCircle.Contains(mPointsStartTimeSelector[0]))
                                mProgress = (mPointsStartTimeSelector[0].X + mSelectorGap - mHorizontalMargin) / (panel_Middle.Width - 2 * mHorizontalMargin);
                        }
                        else
                        {
                            mProgress = (e.X - mHorizontalMargin) / (panel_Middle.Width - 2 * mHorizontalMargin);
                        }
                    }

                    panel_Middle.Invalidate();
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // panel_Middle 마우스 업 이벤트 발생시 처리
        private void panel_Middle_MouseUp(object sender, MouseEventArgs e)
        {
            // Mainbar - Circle 마우스 업시 처리 =================================================================================
            if (mIsCircleClicking)
            {
                mIsCircleClicking = false;
            }

            // Mainbar - StartTime Selector 마우스 업시 처리 =====================================================================
            if (mIsStartTimeSelectorClicking)
            {
                mIsStartTimeSelectorClicking = false;
            }

            // Mainbar - EndTime Selector 마우스 업시 처리 =======================================================================
            if (mIsEndTimeSelectorClicking)
            {
                mIsEndTimeSelectorClicking = false;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // StartTime 텍스트 박스 키 입력시 처리
        private void txtBox_StartTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            // StartTime 텍스트 박스의 입력 문자 제한 =============================================================================
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                // 엔터 입력시 처리 ==============================================================================================
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {

                    if (mTotalTime != string.Empty)
                    {
                        // StartTime Selector X 좌표 변경
                        if ((float)(mEndTime - mStartTime) / Convert.ToInt32(mTotalTime) < mSelectorGap / (float)(panel_Middle.Width - 2 * mHorizontalMargin))
                        {
                            mPointsStartTimeSelector[0].X = mPointsEndTimeSelector[0].X - mSelectorGap;
                        }
                        else
                        {
                            mPointsStartTimeSelector[0].X = (mStartTime / (float)Convert.ToInt32(mTotalTime)) * (panel_Middle.Width - 2 * mHorizontalMargin) + mHorizontalMargin;
                        }

                        // StartTimeSelector의 X좌표로부터 mStartTime 계산
                        if (mTotalTime != string.Empty)
                            mStartTime = (int)Math.Round((mPointsStartTimeSelector[0].X - mHorizontalMargin) / (panel_Middle.Width - 2 * mHorizontalMargin) * Convert.ToInt32(mTotalTime), 0);


                        // Circle 뒤로 밀고 가기 
                        mStartTimeRatio = (mPointsStartTimeSelector[0].X - mHorizontalMargin) / (panel_Middle.Width - 2 * mHorizontalMargin);

                        if (mStartTimeRatio > mProgress)
                        {
                            mProgress = mStartTimeRatio;
                        }

                        panel_Middle.Invalidate();

                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // EndTime 텍스트 박스 키 입력시 처리
        private void txtBox_EndTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            // StartTime 텍스트 박스의 입력 문자 제한 =============================================================================
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                // 엔터 입력시 처리 ==============================================================================================
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {

                    // End Selector가 TotalTime을 초과하지 못하도록 제한
                    if (mTotalTime != string.Empty && mEndTime > Convert.ToInt32(mTotalTime))
                    {
                        mEndTime = Convert.ToInt32(mTotalTime);
                    }

                    if (mTotalTime != string.Empty)
                    {
                        // EndTime Selector X 좌표 변경
                        if ((float)(mEndTime - mStartTime) / Convert.ToInt32(mTotalTime) < mSelectorGap / (float)(panel_Middle.Width - 2 * mHorizontalMargin))
                        {
                            mPointsEndTimeSelector[0].X = mPointsStartTimeSelector[0].X + mSelectorGap;
                        }
                        else
                        {
                            mPointsEndTimeSelector[0].X = (mEndTime / (float)Convert.ToInt32(mTotalTime)) * (panel_Middle.Width - 2 * mHorizontalMargin) + mHorizontalMargin;
                        }

                        // EndTimeSelector의 X좌표로부터 mEndTime 계산
                        if (mTotalTime != string.Empty)
                            mEndTime = (int)Math.Round((mPointsEndTimeSelector[0].X - mHorizontalMargin) / (panel_Middle.Width - 2 * mHorizontalMargin) * Convert.ToInt32(mTotalTime), 0);


                        // Circle 앞으로 밀고 가기 
                        mEndTimeRatio = (mPointsEndTimeSelector[0].X - mHorizontalMargin) / (panel_Middle.Width - 2 * mHorizontalMargin);

                        if (mEndTimeRatio < mProgress)
                        {
                            mProgress = mEndTimeRatio;
                        }

                        panel_Middle.Invalidate();

                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // StartTime/EndTime 텍스트 박스 포커스 떠날시 처리
        private void txtBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string textBoxName = textBox.Name;

            // 자릿수 두자리로 채우기 ============================================================================================
            string text = (textBox.Text == string.Empty ? "00" : string.Format("{0:D2}", Convert.ToInt16(textBox.Text)));

            // 분, 초 범위 제한 ==================================================================================================
            if (textBoxName.Contains("mm") || textBoxName.Contains("ss"))
            {
                if (Convert.ToInt16(text) > 59)
                {
                    text = "59";
                }
            }

            textBox.Text = text;
        }
        #endregion 이벤트 함수
    }
}
