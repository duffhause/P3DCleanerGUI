using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
//using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SHARModOrganiserGUI.Modules
{
    public class P3D
    {
        public bool compressed = false;
        public bool changesMade = false;


        public int uncompressedLength = 0;

        const int chunksOnRecord = 58;
        private struct Chunk //Structure to hold basic universal chunk data
        {
            public byte[] ChunkType;
            public int ChunkSize;
            public int FullChunkSize;
            public byte[] Data;
        }
        public static byte[][] chunkOrder = new byte[chunksOnRecord][]
        {
            new byte[4] { 0, 112, 0, 0}, //HISTORY_CHUNK 0
            new byte[4] { 48, 112, 0, 0 }, //EXPORTINFO_CHUNK 1
            new byte[4] { 4, 0, 240, 3 }, //TREE_CHUNK 2
            new byte[4] { 7, 0, 240, 3}, //FENCE_CHUNK 3
            new byte[4] { 4, 0, 0, 3}, //INTERSECTION_CHUNK 4
            new byte[4] { 9, 0, 0, 3}, //ROADDATESEGMENT_CHUNK 5
            new byte[4] { 3, 0, 0, 3}, //ROAD_CHUNK 6
            new byte[4] { 11, 0, 0, 3}, //PATH_CHUNK 7
            new byte[4] { 0, 1, 0, 3 }, //FOLLOWCAMERADATA_CHUNK 8
            new byte[4] { 0, 32, 2, 0 }, //TEXTUREFONT_CHUNK 9
            new byte[4] { 13, 128, 1, 0 }, //FRONTENDTEXTBIBLE_CHUNK 10
            new byte[4] { 5, 144, 1, 0 }, //SPRITE_CHUNK 11
            new byte[4] { 0, 128, 1, 0 }, //FRONTENDPROJECT_CHUNK 12
            new byte[4] { 0, 144, 1, 0}, //TEXTURE_CHUNK 13
            new byte[4] { 16, 1, 0, 3}, //SET_CHUNK 14
            new byte[4] { 0, 16, 1, 0}, //SHADER_CHUNK 15 
            new byte[4] { 4, 19, 18, 0}, //OLDVERTEXANIMKEYFRAME_CHUNK 16
            new byte[4] { 0, 16, 18, 0}, //ANIMATION_CHUNK 17
            new byte[4] { 0, 48, 1, 0}, //LIGHT_CHUNK 18
            new byte[4] { 128, 35, 0, 0}, //LIGHTGROUP_CHUNK 19
            new byte[4] { 0, 34, 0, 0}, //CAMERA_CHUNK 20
            new byte[4] { 0, 69, 0, 0}, //SKELETON_CHUNK 21
            new byte[4] { 2, 112, 1, 0}, //OLDBILLBOARDQUADGROUP_CHUNK 22
            new byte[4] { 0, 88, 1, 0}, //PARTICLESYSTEMFACTORY_CHUNK 23
            new byte[4] { 1, 88, 1, 0}, //PARTICLESYSTEM2 24
            new byte[4] { 1, 0, 1, 0 }, //SKIN_CHUNK 25
            new byte[4] { 0, 0, 1, 0}, //MESH_CHUNK 26
            new byte[4] { 1, 16, 2, 0}, //EXPRESSIONGROUP_CHUNK 27
            new byte[4] { 2, 16, 2, 0}, //EXPRESSIONMIXER_CHUNK 28
            new byte[4] { 0, 16, 1, 7}, //PHYSICSOBJECT_CHUNK 29
            new byte[4] { 0, 0, 1, 7}, //COLLISIONOBJECT_CHUNK 30
            new byte[4] { 1, 0, 1, 7}, //COLLISIONVOLUME_CHUNK 31
            new byte[4] { 18, 69, 0, 0}, //COMPOSITEDRAWABLE_CHUNK 32
            new byte[4] { 0, 0, 2, 0}, //ANIMATEDOBJECTFACTORY_CHUNK 33
            new byte[4] { 1, 0, 2, 0}, //ANIMATEDOBJECT 34
            new byte[4] { 0, 1, 18, 0 }, //SCENEGRAPH_CHUNK 35
            new byte[4] { 0, 18, 18, 0}, //OLDFRAMECONTROLLER_CHUNK 36
            new byte[4] { 11, 0, 240, 3}, //WORLDSPHERE_CHUNK 37
            new byte[4] { 13, 0, 240, 3}, //LENSFLARE_CHUNK 38
            new byte[4] { 160, 72, 0, 0}, //MULTICONTROLLER_CHUNK 39
            new byte[4] { 2, 18, 18, 0}, //MULTICONTROLLER2_CHUNK 40
            new byte[4] { 1, 18, 18, 0}, //FRAMECONTROLLER_CHUNK 41
            new byte[4] { 0, 0, 2, 8}, //STATEPROPDATAV1_CHUNK 42
            new byte[4] { 0, 16, 0, 3}, //BREAKABLEOBJECT_CHUNK 43
            new byte[4] { 12, 0, 240, 3}, //ANIM_CHUNK 44
            new byte[4] { 14, 0, 240, 3}, //ANIMDYNAPHYS_CHUNK 45
            new byte[4] { 8, 0, 240, 3}, //ANIMCOLL_CHUNK 46
            new byte[4] { 0, 32, 1, 0}, //GAMEATTR_CHUNK 47
            new byte[4] { 5, 0, 0, 3}, //LOCATOR_CHUNK 48
            new byte[4] { 3, 0, 240, 3}, //INTERSECT_CHUNK 49
            new byte[4] { 0, 0, 240, 3}, //STATICENTITY_CHUNK 50
            new byte[4] { 1, 0, 240, 3}, //STATICPHYS_CHUNK 51
            new byte[4] { 9, 0, 240, 3}, //INSTSTATICENTITY_CHUNK 52
            new byte[4] { 10, 0, 240, 3}, //INSTSTATICPHYS_CHUNK 53
            new byte[4] { 2, 0, 240, 3}, //DYNAPHYS_CHUNK 54
            new byte[4] { 0, 64, 1, 0}, //LOCATOR3_CHUNK 55
            new byte[4] { 2, 6, 0, 3 }, //ATC_CHUNK 56
            new byte[4] { 1, 16, 0, 3 }, //INSTPARTICLESYSTEM_CHUNK 57
        };

        const int HISTORY_INDEX = 0;
        const int EXPORTINFO_INDEX = 1;
        const int SPRITE_INDEX = 11;
        const int TEXTURE_INDEX = 13;
        const int SHADER_INDEX = 15;
        const int LOCATOR_INDEX = 48;
        const int STATICENTITY_INDEX = 50;
        const int STATICPHYS_INDEX = 51;
        const int INSTSTATICENTITY_INDEX = 52;
        const int INSTSTATICPHYS_INDEX = 53;
        const int DYNAPHYS_INDEX = 54;
        const int ANIM_COLL_INDEX = 46;
        const int SKELETON_INDEX = 21;
        const int MESH_INDEX = 26;

        private List<Chunk>[] chunks = new List<Chunk>[chunksOnRecord];


        //Converts next four (little endian) bytes in given FileStream to int
        private int Reader4ToInt(FileStream reader) 
        {
            byte[] tmp = new byte[4];
            reader.Read(tmp, 0, 4);
            return BitConverter.ToInt32(tmp,0);
        }


        //Converts next four (little endian) bytes in reader to int
        private int Byte4ToInt(byte[] arr, int offset) 
        {
            byte[] tmp = new byte[4];
            Array.Copy(arr, offset, tmp, 0, 4);
            return BitConverter.ToInt32(tmp, 0);
        }


        //Returns next 4 bytes from given FileStream to byte array
        private byte[] ReadBytesFromReader (FileStream reader, int l)
		{
            byte[] tmp = new byte[l];
            reader.Read(tmp, 0, l);
            return tmp;
        }


        //Takes a byte array of 12 bytes representing a chunk header and returns ChunkType, The size of data inside the chunk and The entire size of the chunk itself and its child chunks 
        private (byte[], int, int) ReadChunkHeader(byte[] header) 
        {
            byte[] ChunkType = new byte[4];
            Array.Copy(header, 0, ChunkType, 0, 4);
            return (ChunkType, Byte4ToInt(header,4), Byte4ToInt(header, 8));
        }


        //Takes a Chunk structure and formats it for a p3d file
        private static byte[] PackChunk(Chunk chunk)
        {
            byte[] fullChunk = new byte[chunk.FullChunkSize];

            byte[] chunkSize = BitConverter.GetBytes(chunk.ChunkSize);
            byte[] chunkChildSize = BitConverter.GetBytes(chunk.FullChunkSize);

            for (int i = 0; i < 4; i++)
            {
                fullChunk[i] = chunk.ChunkType[i];
            }
            for (int i = 4; i < 8; i++)
            {
                fullChunk[i] = Convert.ToByte(chunkSize[i - 4]);
            }
            for (int i = 8; i < 12; i++)
            {
                fullChunk[i] = Convert.ToByte(chunkChildSize[i - 8]);
            }

            int j = 12;
            foreach (byte b in chunk.Data)
            {
                fullChunk[j] = b;
                j++;
            }
            return fullChunk;
        }


        //Will append bytes from a byte[] Source  to a List<byte> destination 
        private List<byte> AddBytesTo(byte[] source,int sourceIndex, List<byte> destination, int length)
		{
            byte[] tmp = new byte[length];
            Array.Copy(source, sourceIndex, tmp, 0, length);
            foreach (byte b in tmp)
            {
                destination.Add(b);
            }
            return destination;
        }


        //Will take a block from a compressed p3d and decompress it
        private byte[] DecompressBlock(byte[] source, int sourceIndex, byte [] destination, int destinationIndex, int destinationLength )
		{
            int written = 0;
            while (written < destinationLength)
			{
                int unknown = source[sourceIndex++];
                if (unknown <= 15)
				{
                    if (unknown == 0)
					{
                        if (source[sourceIndex] == 0)
						{
                            int unknown2 = 0;
                            do
                            {
                                unknown2 = source[++sourceIndex];
                                unknown += 255;
                            } while (unknown2 == 0);
						}
                        unknown += source[sourceIndex++];
                        Array.Copy(source, sourceIndex, destination,destinationIndex,15);
                        destinationIndex += 15;
                        sourceIndex += 15;
                        written += 15;
                    }
                    do
                    {
                        destination[destinationIndex++] = source[sourceIndex++];
                        ++written;
                        --unknown;
                    } while (unknown > 0);
				} else
				{
                    int unknown2 = unknown % 16;
                    if (unknown2 == 0)
					{
                        int unknown3 = 15;
                        if (source[sourceIndex] == 0)
						{
                            int unknown4;
                            do
                            {
                                unknown4 = source[++sourceIndex];
                                unknown3 += 255;
                            } while (unknown4 == 0);
						}
                        unknown2 += source[sourceIndex++] + unknown3;
					}
                    int unknown5 = Convert.ToInt32(Math.Floor(Convert.ToDecimal(unknown2 / 4)));
                    int unknown6 = destinationIndex - (Convert.ToInt32(Math.Floor(Convert.ToDecimal(unknown / 16))) | 16 * source[sourceIndex++]);
                    do
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            destination[destinationIndex++] = destination[unknown6++];
                        }
                        --unknown5;
                    } while (unknown5 > 0);
                    int unknown7 = unknown2 % 4;
                    while (unknown7 > 0)
					{
                        destination[destinationIndex++] = destination[unknown6++];
                        --unknown7;
                    }
                    written += unknown2;
				}
			}
            return destination;
		}
        

        //Will take a compressed P3D and decompress it
        private byte[] DecompressP3D(FileStream reader)
		{
            byte[] magic = ReadBytesFromReader(reader, 4);
            byte[] decompressedData;
            if (magic[3] == 90)
            {
                compressed = true;
                uncompressedLength = Reader4ToInt(reader);
                decompressedData = new byte[uncompressedLength];
                int decompressedLength = 0;
                reader.Position = 8;
                while (decompressedLength < uncompressedLength)
                {
                    int compressedLength = Reader4ToInt(reader);
                    int uncompressedBlockLength = Reader4ToInt(reader);
                    byte[] Data = new byte[compressedLength];
                    reader.Read(Data, 0, compressedLength);
                    decompressedData = DecompressBlock(Data, 0, decompressedData, decompressedLength, uncompressedBlockLength);
                    decompressedLength += uncompressedBlockLength;
                }
            } else
			{
                reader.Position = 8;
                uncompressedLength = Reader4ToInt(reader);
                decompressedData = new byte[uncompressedLength];
                reader.Position = 0;
                reader.Read(decompressedData, 0, uncompressedLength);
                
            }
            return decompressedData;
        }


        //Will set changesMade to true after a change, this means we can check if the file has been changed before writting
        private void SetChangesMade()
		{
            if (!changesMade)
			{
                changesMade = true;
			}
		}


        //Will pad all nummbers in a string so string.Compare works
        private string PadNumbers (string input)
		{
            return Regex.Replace(input, "[0-9]+", match => match.Value.PadLeft(5, '0'));
        }


        //BubbleSort chunks by name
        private List<Chunk> BubbleSortChunksByName(List<Chunk> c)
        {
            bool noSwap;
            int i = 0;
            do
            {
                noSwap = false;
                for (int j = 0; j < c.Count -i- 1; j++)
                {
                    string q = PadNumbers(GetChunkName(c[j]));
                    string p = PadNumbers(GetChunkName(c[j + 1]));
                    if (string.Compare(q, p) > 0)
                    {
                        Chunk tmp = c[j];
                        c[j] = c[j + 1];
                        c[j + 1] = tmp;
                        noSwap = true;
                    }
                }
                i++;
            } while (noSwap);
            return c;
            
        }


        //Will add a History chunk to the chunks, with each string in a string array being a line
        public void AddHistory(string[] lines)
        {
            if (lines.Length == 0)
			{
                return;
			}
            int currentChunkLength = 14;

            List<byte> data = new List<byte>();

            byte[] lineCount = BitConverter.GetBytes(Convert.ToInt16(lines.Length));
            data = AddBytesTo(lineCount, 0, data, 2);

            for (int i = 0; i < lines.Length; i++)
            {
                int padding = lines[i].Length % 4;
                if (padding != 0)
                {
                    padding = 4 - padding;
                }
                byte a = Convert.ToByte(lines[i].Length + padding);
                data.Add(a);
                currentChunkLength++;
                for (int j = 0; j < lines[i].Length+padding; j++)
                {
                    if (j < lines[i].Length)
                    {
                        data.Add(Convert.ToByte(lines[i][j]));
                    }
                    else
                    {
                        data.Add(Convert.ToByte(0));
                    }
                    currentChunkLength++;
                }

                
            }

            byte[] chunkData = data.ToArray();

            Chunk newHistory = new Chunk();

            newHistory.ChunkType = chunkOrder[HISTORY_INDEX];
            newHistory.ChunkSize = currentChunkLength;
            newHistory.FullChunkSize = currentChunkLength;
            newHistory.Data = chunkData;

            uncompressedLength += currentChunkLength;

            chunks[HISTORY_INDEX].Insert(0, newHistory);
            SetChangesMade();
        }


        //Will remove all history chunks
        public void RemoveHistoryChunks()
        {
            List<int> remove = new List<int>();
            int idx = 0;

            foreach (Chunk chunk in chunks[0])
            {
                    uncompressedLength = uncompressedLength - chunk.FullChunkSize;
                    remove.Add(idx);
                    SetChangesMade();
                
                idx++;
            }

            int currentlyRemoved = 0;
            foreach (int i in remove)
            {
                chunks[0].RemoveAt(i - currentlyRemoved);
                currentlyRemoved++;
            }
        }

        private string GetChunkName(Chunk c)
        {
            int offset = c.Data[0];
            if (offset == 0)
            {
                return "";
            }
            offset++;
            byte[] tmp = new byte[offset];
            Array.Copy(c.Data, 1, tmp, 0, offset);
            return System.Text.Encoding.UTF8.GetString(tmp);
        }

        private int GetLocatorType(Chunk locator)
		{
            return locator.Data[locator.Data[0] + 1];
		}

        public void LexographChunks()
        {
            foreach (int idx in new int[12] {LOCATOR_INDEX ,SPRITE_INDEX, TEXTURE_INDEX, SHADER_INDEX, STATICENTITY_INDEX, STATICPHYS_INDEX, INSTSTATICENTITY_INDEX, INSTSTATICPHYS_INDEX, DYNAPHYS_INDEX, ANIM_COLL_INDEX, SKELETON_INDEX, MESH_INDEX })
			{
                Chunk[] tmpComparison = new Chunk[chunks[idx].Count]; 
                chunks[idx].CopyTo(tmpComparison);
                chunks[idx] = BubbleSortChunksByName(chunks[idx]);
                if (idx == LOCATOR_INDEX)
				{
                    bool noSwap;
                    int i = 0;
                    do
                    {
                        noSwap = false;
                        for (int j = 0; j < chunks[idx].Count - i - 1; j++)
                        {
                            int q = GetLocatorType(chunks[idx][j]);
                            int p = GetLocatorType(chunks[idx][j + 1]);
                            if (q > p)
                            {
                                Chunk tmp = chunks[idx][j];
                                chunks[idx][j] = chunks[idx][j + 1];
                                chunks[idx][j + 1] = tmp;
                                noSwap = true;
                            }
                        }
                        i++;
                    } while (noSwap);
                }
                
              
                if (!chunks[idx].SequenceEqual(tmpComparison))
				{
                    SetChangesMade();
				}
				
            }
        }

        public int ReadP3D(string path)
        {
            for (int i = 0; i < chunksOnRecord; i++) { chunks[i] = new List<Chunk>(); }

            FileStream Reader;
            Reader = File.OpenRead(path);
            byte[] uncompressedData = DecompressP3D(Reader);
            Reader.Close();
            int currentIndex = 12;
            while (currentIndex < uncompressedLength)
			{
                //Init Chunk
                Chunk currentChunk = new Chunk();
                //GetHeader
                byte[] header = new byte[12];
                Array.Copy(uncompressedData, currentIndex, header, 0, 12);
                var headerInfo = ReadChunkHeader(header);

                bool hasChunk = false;
                foreach (byte[] type in chunkOrder)
                {
                    if (headerInfo.Item1.SequenceEqual(type))
                    {
                        hasChunk = true;
                        break;
                    }
                }
                if (!hasChunk)
                {
                    string error = string.Format("Chunk {0} is not supported, please report this to Duffhause\nFile: {1}\nChunk Offset: {2}", BitConverter.ToString(headerInfo.Item1), path, currentIndex);
                    MessageBox.Show(error, "Chunk error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
                currentChunk.ChunkType = headerInfo.Item1;
                currentChunk.ChunkSize = headerInfo.Item2;
                currentChunk.FullChunkSize = headerInfo.Item3;
                currentChunk.Data = new byte[currentChunk.FullChunkSize-12];
                currentIndex += 12;
                //GetData
                Array.Copy(uncompressedData, currentIndex, currentChunk.Data, 0, currentChunk.FullChunkSize-12);
                currentIndex += currentChunk.FullChunkSize - 12;
                //Place it in correct ChunkList
                for (int i=0; i<chunksOnRecord; i++)
				{
                    if (currentChunk.ChunkType.SequenceEqual(chunkOrder[i]))
					{
                        chunks[i].Add(currentChunk);
					}
				}
                
            }
            return 0;
        }

        public void WriteP3D(string path)
        {
            if (!changesMade) { return;}
            FileInfo fi = new FileInfo(path);
            DialogResult dr = DialogResult.Retry;
            while (fi.IsReadOnly)
            {
                dr = MessageBox.Show(string.Format("The following file is read only\n{0}", path), "", MessageBoxButtons.AbortRetryIgnore);
                if (dr == DialogResult.Retry)
                {
                    fi = new FileInfo(path);
                }
                else if (dr == DialogResult.Ignore)
                {
                    return;
                } else if (dr == DialogResult.Abort)
				{
                    Application.Exit();
				}
            }
 
            //ADD ERROR HANDLER FOR FILE IS READ ONLY
            
            byte[] output = new byte[uncompressedLength];
            //Array.Copy( SoucreArray, SourceIndex, DestinationArray, DestinationIndex, length )
            Array.Copy(new byte[8] {80, 51, 68, 255, 12, 0, 0, 0}, 0, output, 0, 8);
            Array.Copy(BitConverter.GetBytes(uncompressedLength), 0, output, 8, 4);
            int outputIndex = 12;
            foreach (List<Chunk> lC in chunks)
			{
                foreach(Chunk c in lC)
				{
                    Array.Copy(c.ChunkType, 0, output, outputIndex, 4);
                    outputIndex += 4;
                    Array.Copy(BitConverter.GetBytes(c.ChunkSize), 0, output, outputIndex, 4);
                    outputIndex += 4;
                    Array.Copy(BitConverter.GetBytes(c.FullChunkSize), 0, output, outputIndex, 4);
                    outputIndex += 4;
                    Array.Copy(c.Data, 0, output, outputIndex, c.FullChunkSize-12);
                    outputIndex += c.FullChunkSize - 12;
                }
			}
            dr = DialogResult.Retry;
            while (dr == DialogResult.Retry)
                try
                {
                    BinaryWriter outputWriter = new BinaryWriter(File.Open(path, FileMode.Create, FileAccess.Write));
                    outputWriter.Write(output);
                    outputWriter.Close();
                    break;
                }

                catch (IOException)
                {
                    dr = MessageBox.Show(string.Format("The following file is unavailable because it is:\nstill being written to or \nbeing processed by another thread or \ndoes not exist (has already been processed)\n{0}", path), "", MessageBoxButtons.RetryCancel);
                    if (dr == DialogResult.Cancel)
                    {
                        return;
                    }
                }

                catch (System.UnauthorizedAccessException)
                {
                    dr = MessageBox.Show(string.Format("Access to the following file is denied\nIt is probably set to ReadOnly\n{0}", path), "", MessageBoxButtons.AbortRetryIgnore);
                    if (dr == DialogResult.Retry)
                    {
                        
                    }
                    else if (dr == DialogResult.Ignore)
                    {
                        return;
                    }
                    else if (dr == DialogResult.Abort)
                    {
                        Application.Exit();
                    }
                }

        }
    }
}