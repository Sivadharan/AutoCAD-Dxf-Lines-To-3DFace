using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathLib;

namespace DxfTo3DFace
{
    public partial class frmLineTo3DFace : Form
    {
        List<string> DxfTextList;
        List<Line> LineList;
        List<Line> DeletedLineList;
        List<ThreeDFace> ThreeDFaceList;
        List<string> ThreeDFaceHeaderTextList;
        int ThreeDFaceEntLocation;
        int HandSeed, HandSeedLocation;
        string FaceLayerName;

        public frmLineTo3DFace()
        {
            InitializeComponent();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (File.Exists(txtFileName.Text) == false)
            {
                MessageBox.Show("Please select dxf file");
                return;
            }
            GetDxfLineListAndFace();
            RemoveDuplicateLines();
            for (int i = 0; i <= ThreeDFaceList.Count - 1; i++)
            {
                ThreeDFace ThreeDFace = ThreeDFaceList[i];
                SearchThreeDFace(ThreeDFace);
                lblStatus.Text = "Processing 3DFace: " + ThreeDFaceList.Count;
                statusStrip1.Refresh();
            }
            lblStatus.Text = "";
            //WriteThreeDFaceToTextFile();
            if (rad3DFaceFile.Checked)
            {
                WriteThreeDFaceToDxfFile();
            }
            else
            {
                InsertThreeDFacesToDxfFile();
            }
            MessageBox.Show("Done");
        }

        private void GetDxfLineListAndFace()
        {
            DxfTextList = File.ReadAllLines(txtFileName.Text).ToList();

            LineList = new List<Line>();
            ThreeDFaceList = new List<ThreeDFace>();
            DeletedLineList = new List<Line>();
            ThreeDFaceHeaderTextList = new List<string>();

            int TextLine = 0;
            ThreeDFaceEntLocation = 0;
            HandSeed = 0;
            HandSeedLocation = 0;
            foreach (string DxfText in DxfTextList)
            {
                if (DxfText.Trim() == "AcDbLine")
                {
                    Pnt p1 = new Pnt();
                    p1.x = Convert.ToDecimal(DxfTextList[TextLine + 2].ToString().Trim());
                    p1.y = Convert.ToDecimal(DxfTextList[TextLine + 4].ToString().Trim());
                    p1.z = Convert.ToDecimal(DxfTextList[TextLine + 6].ToString().Trim());
                    Pnt p2 = new Pnt();
                    p2.x = Convert.ToDecimal(DxfTextList[TextLine + 8].ToString().Trim());
                    p2.y = Convert.ToDecimal(DxfTextList[TextLine + 10].ToString().Trim());
                    p2.z = Convert.ToDecimal(DxfTextList[TextLine + 12].ToString().Trim());
                    Line Line = new Line(p1, p2);
                    Line.id = LineList.Count();
                    LineList.Add(Line);
                }

                if ((DxfText.Trim() == "AcDbFace") && (ThreeDFaceList.Count <= 0))
                {
                    ThreeDFace ThreeDFace = new ThreeDFace();
                    ThreeDFace.Pnts = new List<Pnt>();
                    Pnt Pnt = new Pnt();
                    Pnt.x = Convert.ToDecimal(DxfTextList[TextLine + 2].ToString().Trim());
                    Pnt.y = Convert.ToDecimal(DxfTextList[TextLine + 4].ToString().Trim());
                    Pnt.z = Convert.ToDecimal(DxfTextList[TextLine + 6].ToString().Trim());
                    ThreeDFace.Pnts.Add(Pnt);

                    Pnt = new Pnt();
                    Pnt.x = Convert.ToDecimal(DxfTextList[TextLine + 8].ToString().Trim());
                    Pnt.y = Convert.ToDecimal(DxfTextList[TextLine + 10].ToString().Trim());
                    Pnt.z = Convert.ToDecimal(DxfTextList[TextLine + 12].ToString().Trim());
                    ThreeDFace.Pnts.Add(Pnt);

                    Pnt = new Pnt();
                    Pnt.x = Convert.ToDecimal(DxfTextList[TextLine + 14].ToString().Trim());
                    Pnt.y = Convert.ToDecimal(DxfTextList[TextLine + 16].ToString().Trim());
                    Pnt.z = Convert.ToDecimal(DxfTextList[TextLine + 18].ToString().Trim());
                    ThreeDFace.Pnts.Add(Pnt);

                    if (DxfTextList[TextLine + 19].ToString().Trim() == "13")
                    {
                        Pnt = new Pnt();
                        Pnt.x = Convert.ToDecimal(DxfTextList[TextLine + 20].ToString().Trim());
                        Pnt.y = Convert.ToDecimal(DxfTextList[TextLine + 22].ToString().Trim());
                        Pnt.z = Convert.ToDecimal(DxfTextList[TextLine + 24].ToString().Trim());
                        ThreeDFace.Pnts.Add(Pnt);
                    }

                    ThreeDFaceList.Add(ThreeDFace);
                }

                if ((ThreeDFaceEntLocation <= 0) &&
                   (DxfTextList[TextLine].Trim() == "0") && (DxfTextList[TextLine + 1].Trim() == "3DFACE"))
                {
                    ThreeDFaceEntLocation = TextLine;
                    for (int j = TextLine; j <= DxfTextList.Count - 6; j++)
                    {
                        if (DxfTextList[j].Trim() == "8")
                        {
                            FaceLayerName = DxfTextList[j + 1].Trim();
                        }
                        ThreeDFaceHeaderTextList.Add(DxfTextList[j]);
                        if (DxfTextList[j].Trim() == "AcDbFace")
                        {
                            break;
                        }
                    }
                }

                if ((ThreeDFaceEntLocation <= 0) && (DxfTextList[TextLine].Trim() == "$HANDSEED"))
                {
                    HandSeed = int.Parse(DxfTextList[TextLine + 2].Trim(), System.Globalization.NumberStyles.HexNumber);
                    HandSeedLocation = TextLine + 2;
                }

                TextLine++;
            }
        }

        private void RemoveDuplicateLines()
        {
            List<Line> ClonedLineList = new List<Line>(LineList);
            foreach (Line Line in ClonedLineList)
            {
                List<int> LineIdList = (from Line1
                                          in LineList
                                        where (Line.id != Line1.id) &&
                                              (Utils.IsSameLine(Line1, Line) == true)
                                        select Line1.id).ToList();
                foreach (int Id in LineIdList)
                {
                    LineList.RemoveAll(ln => ln.id == Id);
                }
            }
        }

        private Pnt GetPreFacePoint(ThreeDFace ThreeDFace, int FaceNode)
        {
            int PreNode = FaceNode - 1;
            return PreNode == -1 ? ThreeDFace.Pnts[ThreeDFace.Pnts.Count - 2] : ThreeDFace.Pnts[PreNode];
        }

        private void SearchThreeDFace(ThreeDFace ThreeDFace)
        {
            for (int FaceNode = 0; FaceNode <= ThreeDFace.Pnts.Count - 2; FaceNode++)
            {
                Pnt p1 = ThreeDFace.Pnts[FaceNode + 1];
                Pnt p2 = ThreeDFace.Pnts[FaceNode];
                Pnt PreFacePoint = GetPreFacePoint(ThreeDFace, FaceNode);
                Line Line = new Line(p1, p2);
                List<ThreeDFaceBuilder> ThreeDFaceBuilderList = new List<ThreeDFaceBuilder>();
                ThreeDFaceBuilder ThreeDFaceBuilderInit = new ThreeDFaceBuilder();
                ThreeDFaceBuilderInit.AddPoints(p1);
                ThreeDFaceBuilderInit.AddPoints(p2);
                ThreeDFaceBuilderList.Add(ThreeDFaceBuilderInit);
                for (int i = 1; i <= 3; i++)
                {
                    List<ThreeDFaceBuilder> ThreeDFaceBuilderListNew = new List<ThreeDFaceBuilder>();
                    for (int j = 0; j <= ThreeDFaceBuilderList.Count - 1; j++)
                    {
                        ThreeDFaceBuilder ThreeDFaceBuilder = ThreeDFaceBuilderList[j];
                        List<Pnt> PntList = (from Line1
                                               in LineList
                                             where (Utils.PointInLine(Line1, ThreeDFaceBuilder.LastPnt) == true) &&
                                                   (Utils.IsSameLine(new Line(ThreeDFaceBuilder.PreLastPnt, ThreeDFaceBuilder.LastPnt), Line1) == false) &&
                                                   (Utils.IsSameLine(new Line(ThreeDFaceBuilder.LastPnt, PreFacePoint), Line1) == false)
                                             select Utils.AnotherPointInLine(Line1, ThreeDFaceBuilder.LastPnt)).ToList();
                        foreach (Pnt Pnt in PntList)
                        {
                            ThreeDFaceBuilder ThreeDFaceBuilderNew = new ThreeDFaceBuilder();
                            foreach (Pnt Pnt2 in ThreeDFaceBuilder.Pnts)
                            {
                                ThreeDFaceBuilderNew.AddPoints(Pnt2);
                            }
                            ThreeDFaceBuilderNew.AddPoints(Pnt);
                            ThreeDFaceBuilderListNew.Add(ThreeDFaceBuilderNew);
                        }
                    }
                    ThreeDFaceBuilderList = ThreeDFaceBuilderListNew;
                }
                ThreeDFaceBuilder ThreeDFaceBuilderPass1 = (from ThreeDFaceBuilderSelect
                                                         in ThreeDFaceBuilderList
                                                            where ThreeDFaceBuilderSelect.FaceComplete == true
                                                            orderby ThreeDFaceBuilderSelect.Pnts.Count
                                                            select ThreeDFaceBuilderSelect).FirstOrDefault();
                ThreeDFaceBuilder ThreeDFaceBuilderPass2;
                if ((ThreeDFaceBuilderPass1 != null) && (ThreeDFaceBuilderPass1.Pnts.Count == 5))
                {
                    ThreeDFaceBuilderPass2 = (from DeletedLine
                                                in DeletedLineList
                                              where (Utils.IsSameLine(DeletedLine, new Line(ThreeDFaceBuilderPass1.Pnts[0], ThreeDFaceBuilderPass1.Pnts[2])) == true) ||
                                                    (Utils.IsSameLine(DeletedLine, new Line(ThreeDFaceBuilderPass1.Pnts[1], ThreeDFaceBuilderPass1.Pnts[3])) == true)
                                              select 1).Count() > 0 ? null : ThreeDFaceBuilderPass1;
                }
                else
                {
                    ThreeDFaceBuilderPass2 = ThreeDFaceBuilderPass1;
                }
                if (ThreeDFaceBuilderPass2 != null)
                {
                    ThreeDFaceList.Add(ThreeDFaceBuilderPass2.GetThreeDFace());
                    int Id = (from Line1
                                in LineList
                              where Utils.IsSameLine(Line1, Line) == true
                              select Line1.id).FirstOrDefault();
                    DeletedLineList.Add(new Line(Line.p1, Line.p2));
                    LineList.RemoveAll(ln => ln.id == Id);
                }
            }
        }

        private void WriteThreeDFaceToTextFile()
        {
            List<string> TextList = new List<string>();
            foreach (ThreeDFace ThreeDFace in ThreeDFaceList)
            {
                foreach (Pnt Pnt in ThreeDFace.Pnts)
                {
                    TextList.Add(string.Join(",", Pnt.xyz.Select(n => n.ToString()).ToArray()));
                }
                TextList.Add("");
            }
            File.WriteAllLines(Path.GetDirectoryName(txtFileName.Text) + "/ThreeDFace.txt", TextList);
        }

        private void WriteThreeDFaceToDxfFile()
        {
            List<string> TextList = new List<string>();
            TextList.Add(" 0");
            TextList.Add("SECTION");
            TextList.Add(" 2");
            TextList.Add("ENTITIES");
            foreach (ThreeDFace ThreeDFace in ThreeDFaceList)
            {
                TextList.Add(" 0");
                TextList.Add("3DFACE");
                TextList.Add(" 8");
                TextList.Add(FaceLayerName);
                for (int i = 0; i <= 3; i++)
                {
                    Pnt Pnt = ThreeDFace.Pnts[i];
                    for (int j = 0; j <= 2; j++)
                    {
                        TextList.Add(" " + ((10 * (j + 1)) + i).ToString());
                        TextList.Add(Pnt.xyz[j].ToString());
                    }
                }
            }
            TextList.Add(" 0");
            TextList.Add("ENDSEC");
            TextList.Add(" 0");
            TextList.Add("EOF");

            File.WriteAllLines(Path.GetDirectoryName(txtFileName.Text) + "/3DFace.dxf", TextList);
        }

        private void InsertThreeDFacesToDxfFile()
        {
            List<string> TextList = new List<string>();
            //First 3dFace is already there in the file. So, should not insert again!
            for (int FaceCount = 1; FaceCount <= ThreeDFaceList.Count - 1; FaceCount++)
            {
                ThreeDFace ThreeDFace = ThreeDFaceList[FaceCount];
                List<string> ThreeDFaceHeaderTextListNew = new List<string>(ThreeDFaceHeaderTextList);
                HandSeed = (HandSeed + FaceCount - 1);
                ThreeDFaceHeaderTextListNew[3] = HandSeed.ToString("X");
                TextList.AddRange(ThreeDFaceHeaderTextListNew);
                for (int i = 0; i <= 3; i++)
                {
                    Pnt Pnt = ThreeDFace.Pnts[i];
                    for (int j = 0; j <= 2; j++)
                    {
                        TextList.Add(" " + ((10 * (j + 1)) + i).ToString());
                        TextList.Add(Pnt.xyz[j].ToString());
                    }
                }
            }

            DxfTextList[HandSeedLocation] = (HandSeed + 2).ToString("X");

            DxfTextList.InsertRange(ThreeDFaceEntLocation, TextList);
            string OutFileName = txtFileName.Text;
            OutFileName = OutFileName.Replace(".dxf", "_with 3DFace.dxf");
            File.WriteAllLines(OutFileName, DxfTextList);
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = false;
            openFileDialog1.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            openFileDialog1.ShowDialog();
            txtFileName.Text = openFileDialog1.FileName;
        }
    }
}
