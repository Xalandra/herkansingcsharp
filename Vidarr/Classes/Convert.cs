using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media.MediaProperties;
using Windows.Media.Transcoding;
using Windows.Storage;
using Windows.UI.Popups;

namespace Vidarr
{
    public partial class Convert
    {
        AsyncLock bouncer = new AsyncLock();

        void TranscodeComplete(IAsyncActionWithProgress<double> asyncInfo, AsyncStatus status)
        {
            asyncInfo.GetResults();
        }

            /// <summary>
            /// Converts the chosen media to a certain extension the user has chosen for
            /// </summary>
            /// <param name="selectedFiles"></param>
            /// <param name="output"></param>
        public async void ConvertChosenMedia(List<StorageFile> selectedFiles, string output)
        {
            List<StorageFile> files = selectedFiles;
            foreach (StorageFile file in files)
            {
                StorageFile source = file;
                MediaEncodingProfile profile = new MediaEncodingProfile();

                var ext = file.FileType;

                //To which extension needs the file be converted to
                switch (output)
                {
                    case "MP3":
                        if(ext == ".MP3")
                        {
                            ext = null;
                            MessageDialog showDialogError = new MessageDialog("Het bestand kan niet naar dezelfde extensie geconverteerd worden.");
                            await showDialogError.ShowAsync();
                        }
                        else
                        {
                            profile = MediaEncodingProfile.CreateMp3(AudioEncodingQuality.High);
                        }

                        break;
                    case "MP4":
                        if (ext == ".MP4")
                        {
                            ext = null;
                            MessageDialog showDialogError = new MessageDialog("Het bestand kan niet naar dezelfde extensie geconverteerd worden.");
                            await showDialogError.ShowAsync();
                        }
                        else
                        {
                            profile = MediaEncodingProfile.CreateMp4(VideoEncodingQuality.Auto);
                        }
                        break;
                    case "WAV":
                        if (ext == ".WAV")
                        {
                            ext = null;
                            MessageDialog showDialogError = new MessageDialog("Het bestand kan niet naar dezelfde extensie geconverteerd worden.");
                            await showDialogError.ShowAsync();
                        }
                        profile = MediaEncodingProfile.CreateWav(AudioEncodingQuality.High);
                        break;
                    default:
                        output = null;
                        MessageDialog showDialogUnknown = new MessageDialog("Gekozen formaat om naar te converten is niet geldig.");
                        await showDialogUnknown.ShowAsync();
                        break;

                }
                
                //Puts the file in the "Video's" folder
                if (output != null && ext != null)
                {
                    var folder = KnownFolders.VideosLibrary;
                    string datetime = "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
                    var destination = await folder.CreateFileAsync(source.Name.Remove(source.Name.Length - 4) + datetime + "." + output);

                    MediaTranscoder transcoder = new MediaTranscoder();

                    PrepareTranscodeResult prepareOp = null;
                    while (source.Equals(null) || destination.Equals(null) || profile.Equals(null)) {
                        //waiting
                    }
                    using (await bouncer.LockAsync())
                    {
                        //catch the nullreference exception on prepareOp
                        try
                        {
                            prepareOp = await transcoder.PrepareFileTranscodeAsync(source, destination, profile);
                            //if possible
                            if (prepareOp.CanTranscode)
                            {
                                var transcodeOp = prepareOp.TranscodeAsync();

                                transcodeOp.Completed +=
                                    new AsyncActionWithProgressCompletedHandler<double>(TranscodeComplete);
                            }
                            else
                            {
                                switch (prepareOp.FailureReason)
                                {
                                    case TranscodeFailureReason.CodecNotFound:
                                        Debug.WriteLine("Codec not found.");
                                        break;
                                    case TranscodeFailureReason.InvalidProfile:
                                        Debug.WriteLine("Invalid profile.");
                                        break;
                                    default:
                                        Debug.WriteLine("Unknown failure.");
                                        break;
                                }
                            }
                        }
                        catch (NullReferenceException ex) { Debug.WriteLine(ex.Message); }
                    }
                }
            }
            
        }
    }
}
