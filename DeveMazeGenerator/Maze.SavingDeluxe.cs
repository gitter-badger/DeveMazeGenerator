﻿using DeveMazeGenerator.Generators;
using DeveMazeGenerator.InnerMaps;
using Hjg.Pngcs;
using Hjg.Pngcs.Chunks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveMazeGenerator
{
    public class PathPos
    {
        public int X;
        public int Pos;

        public PathPos(int X, int Pos)
        {
            this.X = X;
            this.Pos = Pos;
        }
    }

    public partial class Maze
    {
        /// <summary>
        /// Saves the maze with a specified path as PNG
        /// Uses more memory then saving without a path (depending on the selected MazeSaveType)
        /// </summary>
        /// <param name="fileName">The filename of the file</param>
        /// <param name="path">The path (can be generated by calling PathFinderDepthFirst.GoFind)</param>
        public void SaveMazeAsImageDeluxe(String fileName, List<MazePoint> path, Action<int, int> lineSavingProgress)
        {
            if (lineSavingProgress == null)
            {
                lineSavingProgress = (cur, tot) => { };
            }

            List<List<PathPos>> pathPosArray = new List<List<PathPos>>(this.Height - 1);
            for (int y = 0; y < this.Height - 1; y++)
            {
                pathPosArray.Add(new List<PathPos>());
                Queue<String> aaa = new Queue<string>();
            }

            for (int i = 0; i < path.Count; i++)
            {
                var curMazePoint = path[i];
                var pathPos = new PathPos(curMazePoint.X, i);
                pathPosArray[curMazePoint.Y].Add(pathPos);
            }




            ImageInfo imi = new ImageInfo(this.Width - 1, this.Height - 1, 8, false); // 8 bits per channel, no alpha 
            // open image for writing 
            PngWriter png = FileHelper.CreatePngWriter(fileName, imi, true);
            // add some optional metadata (chunks)
            png.GetMetadata().SetDpi(100.0);
            png.GetMetadata().SetTimeNow(0); // 0 seconds fron now = now
            png.GetMetadata().SetText(PngChunkTextVar.KEY_Title, "Just a text image");
            PngChunk chunk = png.GetMetadata().SetText("my key", "my text .. bla bla");
            chunk.Priority = true; // this chunk will be written as soon as possible



            for (int y = 0; y < this.Height - 1; y++)
            {
                ImageLine iline = new ImageLine(imi);
                var curRow = pathPosArray[y];

                for (int x = 0; x < this.Width - 1; x++)
                {
                    var curPathPos = curRow.FirstOrDefault(t => t.X == x);

                    int r = 0;
                    int g = 0;
                    int b = 0;
                    if (curPathPos != null)
                    {

                        int formulathing = (int)((double)curPathPos.Pos / (double)path.Count * 255.0);
                        r = formulathing;
                        g = 255 - formulathing;
                        b = 0;
                    }
                    else if (this.innerMap[x, y])
                    {
                        r = 255;
                        g = 255;
                        b = 255;
                    }


                    ImageLineHelper.SetPixel(iline, x, r, g, b);
                }
                png.WriteRow(iline, y);
                lineSavingProgress(y, this.Height - 1);
            }
            png.End();


            //for (int col = 0; col < imi.Cols; col++)
            //{ // this line will be written to all rows  
            //    int r = 255;
            //    int g = 127;
            //    int b = 255 * col / imi.Cols;
            //    ImageLineHelper.SetPixel(iline, col, r, g, b); // orange-ish gradient
            //}
            //for (int row = 0; row < png.ImgInfo.Rows; row++)
            //{
            //    png.WriteRow(iline, row);
            //}
            //png.End();

        }
    }
}