using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

//using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace P3DCleaner.Modules
{
    public partial class P3D
    {
        public bool compressed = false;
        public bool changesMade = false;

        public string fileName;
        public string p3dHash;

        public int uncompressedLength = 0;

        private const int chunksOnRecord = 58;

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

        private const int HISTORY_INDEX = 0;
        private const int EXPORTINFO_INDEX = 1;
        private const int SPRITE_INDEX = 11;
        private const int TEXTURE_INDEX = 13;
        private const int SHADER_INDEX = 15;
        private const int SET_INDEX = 14;
        private const int LOCATOR_INDEX = 48;
        private const int STATICENTITY_INDEX = 50;
        private const int STATICPHYS_INDEX = 51;
        private const int INSTSTATICENTITY_INDEX = 52;
        private const int INSTSTATICPHYS_INDEX = 53;
        private const int DYNAPHYS_INDEX = 54;
        private const int ANIM_COLL_INDEX = 46;
        private const int SKELETON_INDEX = 21;
        private const int MESH_INDEX = 26;

        public struct Chunk //Structure to hold basic chunk data
        {
            public byte[] ChunkType;
            public int ChunkSize;
            public int FullChunkSize;
            public byte[] Data;
            public List<Chunk> subChunks;
        }
        public Chunk Root = new Chunk();
        private Chunk MisplacedChunks = new Chunk();


        //Converts next four (little endian) bytes in given FileStream to int
        private int Reader4ToInt(FileStream reader)
        {
            byte[] tmp = new byte[4];
            reader.Read(tmp, 0, 4);
            return BitConverter.ToInt32(tmp, 0);
        }

        //Converts next four (little endian) bytes in reader to int
        private int Byte4ToInt(byte[] arr, int offset)
        {
            byte[] tmp = new byte[4];
            Array.Copy(arr, offset, tmp, 0, 4);
            return BitConverter.ToInt32(tmp, 0);
        }

        //Returns next 4 bytes from given FileStream to byte array
        private byte[] ReadBytesFromReader(FileStream reader, int l)
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
            return (ChunkType, Byte4ToInt(header, 4), Byte4ToInt(header, 8));
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

            if (chunk.subChunks != null)
            {
                foreach (Chunk subChunk in chunk.subChunks)
                {
                    foreach (byte b in PackChunk(subChunk))
                    {
                        fullChunk[j] = b;
                        j++;
                    }
                }
            }

            return fullChunk;
        }

        //Will append bytes from a byte[] Source  to a List<byte> destination
        private List<byte> AddBytesTo(byte[] source, int sourceIndex, List<byte> destination, int length)
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
        private byte[] DecompressBlock(byte[] source, int sourceIndex, byte[] destination, int destinationIndex, int destinationLength)
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
                        Array.Copy(source, sourceIndex, destination, destinationIndex, 15);
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
                }
                else
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
        private byte[] DecompressP3D(FileStream Reader)
        {
            byte[] magic = Utility.ReadByte4(Reader);
            byte[] decompressedData;
            if (magic[3] == 90) //If magic word ends in "Z" then it is compressed and need decompressing
            {
                compressed = true;
                uncompressedLength = Utility.ReadUint32(Reader);
                decompressedData = new byte[uncompressedLength];
                int decompressedLength = 0;
                Reader.Position = 8;
                while (decompressedLength < uncompressedLength)
                {
                    int compressedLength = Utility.ReadUint32(Reader);
                    int uncompressedBlockLength = Utility.ReadUint32(Reader);
                    byte[] Data = new byte[compressedLength];
                    Reader.Read(Data, 0, compressedLength);
                    decompressedData = DecompressBlock(Data, 0, decompressedData, decompressedLength, uncompressedBlockLength);
                    decompressedLength += uncompressedBlockLength;
                }
            }
            else
            {
                Reader.Position = 8;
                uncompressedLength = Utility.ReadUint32(Reader);
                decompressedData = new byte[uncompressedLength];
                Reader.Position = 0;
                Reader.Read(decompressedData, 0, uncompressedLength);
            }
            p3dHash = Hash.GetHashSHA1(decompressedData);
            return decompressedData;
        }


        //Will pad all nummbers in a string so string.Compare works
        private string PadNumbers(string input)
        {
            return Regex.Replace(input, "[0-9]+", match => match.Value.PadLeft(5, '0'));
        }

        //Will add a History chunk to the chunks, with each string in a string array being a line
        public Chunk AddHistory(Chunk chunk, string[] lines)
        {
            if (lines.Length == 0)
            {
                return chunk;
            }

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
                for (int j = 0; j < lines[i].Length + padding; j++)
                {
                    if (j < lines[i].Length)
                    {
                        data.Add(Convert.ToByte(lines[i][j]));
                    }
                    else
                    {
                        data.Add(Convert.ToByte(0));
                    }
                }
            }

            byte[] chunkData = data.ToArray();

            Chunk newHistory = new Chunk();

            newHistory.ChunkType = chunkOrder[HISTORY_INDEX];
            newHistory.subChunks = new List<Chunk>();
            newHistory = RecalculateChunk(newHistory, chunkData);


            chunk.subChunks.Insert(0, newHistory);
            return chunk;
        }

        public void RemoveHistoryChunks()
        {
            List<int> remove = new List<int>();
            int idx = 0;

            foreach (Chunk chunk in Root.subChunks)
            {
                if (chunk.ChunkType.SequenceEqual(chunkOrder[HISTORY_INDEX]) || chunk.ChunkType.SequenceEqual(chunkOrder[EXPORTINFO_INDEX]))
                {
                    uncompressedLength = uncompressedLength - chunk.FullChunkSize;
                    remove.Add(idx);
                    idx++;
                }
            }

            int currentlyRemoved = 0;
            foreach (int i in remove)
            {
                Console.WriteLine(i);
                Root.subChunks.RemoveAt(i - currentlyRemoved);
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

        private List<Chunk> BubbleSortChunksByName(List<Chunk> c)
        {
            bool noSwap;
            int i = 0;
            do
            {
                noSwap = false;
                for (int j = 0; j < c.Count - i - 1; j++)
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

        public void DeleteUnexpectedChunksInRoot()
        {  
            List<int> remove = new List<int>();
            for (int i=0; i<Root.subChunks.Count; i++)
            {
                bool unexpected = true;
                foreach (byte[] type in chunkOrder)
                {
                    if (Root.subChunks[i].ChunkType.SequenceEqual(type))
                    {
                        unexpected = false;
                        break;
                    } 
                }

                if (unexpected) remove.Add(i);
            }


            foreach (int rIdx in remove) MisplacedChunks.subChunks.Add(Root.subChunks[rIdx]);

            int removed = 0;
            foreach (int rIdx in remove) Root.subChunks.RemoveAt(rIdx - removed++);

        }

        public List<Chunk>[] ContainChunks(Chunk chunk)
        {
            List<Chunk>[] Container = new List<Chunk>[chunksOnRecord];

            for (int i = 0; i < chunksOnRecord; i++) { Container[i] = new List<Chunk>(); }
            int idx = -1;
            foreach (Chunk currentChunk in chunk.subChunks)
            {
                idx++;
                bool hasChunk = false;
                foreach (byte[] type in chunkOrder)
                {
                    if (currentChunk.ChunkType.SequenceEqual(type))
                    {
                        hasChunk = true;
                        break;
                    }
                }
               
                for (int i = 0; i < chunksOnRecord; i++)
                {
                    if (currentChunk.ChunkType.SequenceEqual(chunkOrder[i]) && hasChunk)
                    {
                        Container[i].Add(currentChunk);
                    }
                }
            }

            return Container;
        }

        public Chunk PackContainer(List<Chunk>[] Container, Chunk baseChunk)
        {
            List<Chunk> packedContainerChunks = new List<Chunk>();
            foreach (List<Chunk> chunkList in Container)
            {
                if (chunkList.Count != 0)
                {
                    foreach (Chunk c in chunkList)
                    {
                        packedContainerChunks.Add(c);
                    }
                }
            }

            baseChunk.subChunks = packedContainerChunks;
            return baseChunk;
        }

        public void LexographChunks()
        {
            List<Chunk>[] Container = ContainChunks(Root);

            foreach (int idx in new int[] { LOCATOR_INDEX, SPRITE_INDEX, TEXTURE_INDEX, SHADER_INDEX, STATICENTITY_INDEX, STATICPHYS_INDEX, INSTSTATICENTITY_INDEX, INSTSTATICPHYS_INDEX, DYNAPHYS_INDEX, ANIM_COLL_INDEX, SKELETON_INDEX, MESH_INDEX })
            {

                Container[idx] = BubbleSortChunksByName(Container[idx]);
                if (idx == LOCATOR_INDEX)
                {
                    bool noSwap;
                    int i = 0;
                    do
                    {
                        noSwap = false;
                        for (int j = 0; j < Container[idx].Count - i - 1; j++)
                        {
                            int q = GetLocatorType(Container[idx][j]);
                            int p = GetLocatorType(Container[idx][j + 1]);
                            if (q > p)
                            {
                                Chunk tmp = Container[idx][j];
                                Container[idx][j] = Container[idx][j + 1];
                                Container[idx][j + 1] = tmp;
                                noSwap = true;
                            }
                        }
                        i++;
                    } while (noSwap);
                }

            }
            Root = PackContainer(Container, Root);
        }

        private byte[] GetRange(byte[] byteArray, int offset, int length)
        {
            byte[] range = new byte[length];
            Array.Copy(byteArray, offset, range, 0, length);
            return range;
        }

        private Chunk GetChunk(byte[] data, int offset)
        {
            Chunk chunk = new Chunk();
            chunk.subChunks = new List<Chunk>();
            byte[] header = new byte[12];
            Array.Copy(data, offset, header, 0, 12);
            var headerInfo = ReadChunkHeader(header);
            chunk.ChunkType = headerInfo.Item1;
            chunk.ChunkSize = headerInfo.Item2;
            chunk.FullChunkSize = headerInfo.Item3;
            chunk.Data = new byte[chunk.ChunkSize - 12];
            offset += 12;
            Array.Copy(data, offset, chunk.Data, 0, chunk.ChunkSize - 12);
            offset += chunk.ChunkSize - 12;
            int currentSize = chunk.ChunkSize;
            while (currentSize < chunk.FullChunkSize)
            {
                var subHeader = ReadChunkHeader(GetRange(data, offset, 12));
                Chunk subChunk = GetChunk(data, offset);
                offset += subHeader.Item3;
                currentSize += subHeader.Item3;
                chunk.subChunks.Add(subChunk);
            }

            return chunk;
        }

        public void ReadP3D(string path)
        {
            FileStream Reader;
            Reader = File.OpenRead(path);
            byte[] uncompressedData = DecompressP3D(Reader);
            Reader.Close();
            Root = GetChunk(uncompressedData, 0);

            MisplacedChunks.ChunkType = chunkOrder[SET_INDEX];
            MisplacedChunks.subChunks = new List<Chunk>();
        }

        private Chunk RecalculateChunk(Chunk currentChunk, byte[] newData)
        {
            int fullChunkSize = 0;
            for (int i = 0; i < currentChunk.subChunks.Count; i++)
            {
                fullChunkSize += currentChunk.subChunks[i].FullChunkSize;
            }
            currentChunk.ChunkSize = newData.Length + 12;
            currentChunk.Data = newData;
            currentChunk.FullChunkSize = fullChunkSize + currentChunk.ChunkSize;
            return currentChunk;
        }

        private void PrepareMisplacedChunks()
        {
            MisplacedChunks = AddHistory(MisplacedChunks, new string[] { "Misplaced chunks", "This P3D has been cleaned using Stoner Team's P3D Cleaner.", "Any chunks we believe have been misplaced in the root have been placed here." });

            string chunkName = "MisplacedChunks";
            int padding = chunkName.Length % 4;
            if (padding != 0)
            {
                padding = 4 - padding;
            }

            byte nameLength = Convert.ToByte(chunkName.Length + padding ) ;
            byte[] data = new byte[nameLength + 6];
            data[0] = nameLength;

            for (int j = 1; j < nameLength; j++)
            {
                if (j <= chunkName.Length)
                {
                    data[j] = (Convert.ToByte(chunkName[j-1]));
                }
                else
                {
                    data[j] = (Convert.ToByte(0));
                }
            }

            byte numOfsubs = Convert.ToByte(0);
            data[nameLength + 5] = numOfsubs;

            MisplacedChunks.Data = data;

            MisplacedChunks = RecalculateChunk(MisplacedChunks, MisplacedChunks.Data);
        }

        public int WriteP3D(string path)
        {

            if (!P3DCleaner.MainWindow.deleteUnexpectedChunks && MisplacedChunks.subChunks.Count > 0)
            {
                PrepareMisplacedChunks();
                Root.subChunks.Insert(0, MisplacedChunks);
            }

            Root = RecalculateChunk(Root, Root.Data);
            uncompressedLength = Root.FullChunkSize;
            if (Root.subChunks.Count == 0)
            {
                Console.WriteLine("File is blank " + path);
                return -1;
            }
            //ADD ERROR HANDLER FOR FILE IS READ ONLY
            
            byte[] output = new byte[uncompressedLength];
            Root.FullChunkSize = uncompressedLength;
            byte[] packedRoot = PackChunk(Root);

            string newP3dHash = Hash.GetHashSHA1(packedRoot);
            
            if (newP3dHash != p3dHash)
            {
                BinaryWriter outputWriter = new BinaryWriter(File.Open(path, FileMode.Create, FileAccess.Write));
                Array.Copy(packedRoot, 0, output, 0, uncompressedLength);
                outputWriter.Write(output);
                outputWriter.Close();
                return 1;
            } else
            {
                return 0;
            }
            
        }
    }
}