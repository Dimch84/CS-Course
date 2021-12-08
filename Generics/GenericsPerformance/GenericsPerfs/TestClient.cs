using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace GenericsPerfs
{
   public class TestClient : Form
   {
      delegate void TestMethod();
     
      Button m_ValueTypesTest;
      Button m_ReferenceTypesTest;
      TextBox m_TextResultBox;
      Label m_TestResultLabel;
      TrackBar m_IterationBar;
      Label m_DurationLabel;
      PictureBox m_TimerBox;
      const long COUNT = 100000;

      public TestClient()
      {
         InitializeComponent();
      }
      #region Windows Form Designer generated code
      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof( TestClient));

         this.m_ValueTypesTest = new System.Windows.Forms.Button();
         this.m_ReferenceTypesTest = new System.Windows.Forms.Button();
         this.m_TextResultBox = new System.Windows.Forms.TextBox();
         this.m_TestResultLabel = new System.Windows.Forms.Label();
         this.m_IterationBar = new System.Windows.Forms.TrackBar();
         this.m_DurationLabel = new System.Windows.Forms.Label();
         this.m_TimerBox = new System.Windows.Forms.PictureBox();
         ((System.ComponentModel.ISupportInitialize) (this.m_IterationBar)).BeginInit();
         ((System.ComponentModel.ISupportInitialize) (this.m_TimerBox)).BeginInit();
         this.SuspendLayout();

         // 
         // m_ValueTypesTest
         // 
         this.m_ValueTypesTest.Location = new System.Drawing.Point(17,34);
         this.m_ValueTypesTest.Name = "m_ValueTypesTest";
         this.m_ValueTypesTest.Size = new System.Drawing.Size(125,27);
         this.m_ValueTypesTest.TabIndex = 0;
         this.m_ValueTypesTest.Text = "Value Types Test";
         this.m_ValueTypesTest.Click += new System.EventHandler(this.OnValueTest);

         // 
         // m_ReferenceTypesTest
         // 
         this.m_ReferenceTypesTest.Location = new System.Drawing.Point(17,84);
         this.m_ReferenceTypesTest.Name = "m_ReferenceTypesTest";
         this.m_ReferenceTypesTest.Size = new System.Drawing.Size(124,32);
         this.m_ReferenceTypesTest.TabIndex = 1;
         this.m_ReferenceTypesTest.Text = "Reference Types Test";
         this.m_ReferenceTypesTest.Click += new System.EventHandler(this.OnReferenceTest);

         // 
         // m_TextResultBox
         // 
         this.m_TextResultBox.Location = new System.Drawing.Point(275,42);
         this.m_TextResultBox.Name = "m_TextResultBox";
         this.m_TextResultBox.Size = new System.Drawing.Size(67,19);
         this.m_TextResultBox.TabIndex = 2;

         // 
         // m_TestResultLabel
         // 
         this.m_TestResultLabel.Location = new System.Drawing.Point(192,34);
         this.m_TestResultLabel.Name = "m_TestResultLabel";
         this.m_TestResultLabel.Size = new System.Drawing.Size(74,34);
         this.m_TestResultLabel.TabIndex = 3;
         this.m_TestResultLabel.Text = "Generics are faster by:";

         // 
         // m_IterationBar
         // 
         this.m_IterationBar.Location = new System.Drawing.Point(192,111);
         this.m_IterationBar.Maximum = 100;
         this.m_IterationBar.Minimum = 1;
         this.m_IterationBar.Name = "m_IterationBar";
         this.m_IterationBar.Size = new System.Drawing.Size(150,42);
         this.m_IterationBar.SmallChange = 10;
         this.m_IterationBar.TabIndex = 4;
         this.m_IterationBar.TickFrequency = 10;
         this.m_IterationBar.Value = 50;

         // 
         // m_DurationLabel
         // 
         this.m_DurationLabel.Location = new System.Drawing.Point(192,84);
         this.m_DurationLabel.Name = "m_DurationLabel";
         this.m_DurationLabel.TabIndex = 5;
         this.m_DurationLabel.Text = "Test Iterations:";

         // 
         // m_TimerBox
         // 
         this.m_TimerBox.Image = ((System.Drawing.Image) (resources.GetObject("m_TimerBox.Image")));
         this.m_TimerBox.Location = new System.Drawing.Point(31,127);
         this.m_TimerBox.Name = "m_TimerBox";
         this.m_TimerBox.Size = new System.Drawing.Size(77,69);
         this.m_TimerBox.TabIndex = 6;
         this.m_TimerBox.TabStop = false;
         this.m_TimerBox.WaitOnLoad = false;

         // 
         // TestClient
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
         this.ClientSize = new System.Drawing.Size(361,215);
         this.Controls.Add(this.m_TimerBox);
         this.Controls.Add(this.m_DurationLabel);
         this.Controls.Add(this.m_IterationBar);
         this.Controls.Add(this.m_TestResultLabel);
         this.Controls.Add(this.m_TextResultBox);
         this.Controls.Add(this.m_ReferenceTypesTest);
         this.Controls.Add(this.m_ValueTypesTest);
         this.Name = "TestClient";
         this.Text = "Generics Performance Test";
         ((System.ComponentModel.ISupportInitialize) (this.m_IterationBar)).EndInit();
         ((System.ComponentModel.ISupportInitialize) (this.m_TimerBox)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();
      }
        #endregion
      
      static void Main()
      {
         Application.Run(new TestClient());
      }
      
      long GetTestTime(TestMethod testMethod)
      {
         Stopwatch stopper = new Stopwatch();

         stopper.Start();
         testMethod();
         stopper.Stop();

         return stopper.ElapsedMilliseconds;
      }

      void OnValueTest(object sender,System.EventArgs e)
      {
         float boxedTime   = GetTestTime(TestValueBoxed);
         float genericTime = GetTestTime(TestValueGeneric);

         float perf = 100 * 1/((genericTime/boxedTime));
         
         m_TextResultBox.Text = Math.Round(perf).ToString() + "%";
      }

      void OnReferenceTest(object sender,System.EventArgs e)
      {
         float boxedTime   = GetTestTime(TestReferenceBoxed);
         float genericTime = GetTestTime(TestReferenceGeneric);

         float perf = 100 * 1/((genericTime/boxedTime));

         m_TextResultBox.Text = Math.Round(perf).ToString() + "%";
      }

      void TestValueBoxed()
      {
            Stack stack = new Stack();

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);

            long temp = 0;
            long iteration = COUNT * m_IterationBar.Value;          
            for(long i = 0;i < iteration;i ++)
            {
                stack.Push(i);
                temp = (long)stack.Pop();
            }
        }

        void TestValueGeneric()
        {
           Stack<long> stack = new Stack<long>();

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);

            long temp = 0;
            DateTime start = DateTime.Now;

            long iteration = COUNT * m_IterationBar.Value;

            for(long i = 0;i < iteration;i ++)
            {
                stack.Push(i);
                temp = stack.Pop();
            }
        }

        void TestReferenceBoxed()
        {
            Stack stack = new Stack();

            stack.Push ("1");
            stack.Push ("2");
            stack.Push ("3");
            stack.Push ("4");

            string temp = String.Empty;

            long iteration = COUNT * m_IterationBar.Value;

            for(long i = 0;i < iteration;i ++)
            {
                stack.Push ("AAAAA");
                temp = (string)stack.Pop();
            }
        }

        void TestReferenceGeneric()
        {
            Stack<string> stack = new Stack<string>();

            stack.Push("1");
            stack.Push("2");
            stack.Push("3");
            stack.Push("4");

            string temp = String.Empty;

            long iteration = COUNT * m_IterationBar.Value;

            for(long i = 0;i < iteration;i ++)
            {
                stack.Push ("AAAAA");
                temp = stack.Pop();
            }
        }
   }
}
