using boooooom.CommonTypes;
using boooooom.Enums;
using NAudio.Wave;

namespace boooooom.FileHelper;

public class MusicPlayer
{
    public static void Play()
    {
        while (BaseProgram.Status != GameStatus.Finished)
        {
            var music = new MusicPlayer();
            music.PlayMusic();
            
            while (BaseProgram.Status == GameStatus.Paused)
            {
                Thread.Sleep(1000);
            }
        }
    }
    
    private void PlayMusic()
    {
        var path = "Resources/Song.wav";
        
        try
        {
            using var audioFile = new Mp3FileReader(path);
            using var outputDevice = new WaveOutEvent();
            
            outputDevice.Init(audioFile);
            outputDevice.Volume = 0.1F;
            outputDevice.Play();
            
            while (BaseProgram.Status != GameStatus.Finished)
            {
                if (BaseProgram.Status == GameStatus.Paused)
                {
                    outputDevice.Stop();
                    break;
                }
                    
                if (outputDevice.PlaybackState == PlaybackState.Stopped)
                {
                    if (BaseProgram.Status == 0)
                    {
                        audioFile.Seek(0, SeekOrigin.Begin);
                        outputDevice.Play();
                    }
                }
                   
                Thread.Sleep(100);
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}