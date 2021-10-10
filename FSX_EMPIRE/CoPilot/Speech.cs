using System;
using System.Speech.Synthesis;

namespace FSX_EMPIRE
{
    class Speech : IDisposable
    {
        readonly SpeechSynthesizer coPilot;
        readonly SpeechSynthesizer crew;

        public Speech()
        {
            coPilot = new SpeechSynthesizer();
            coPilot.SelectVoice("Microsoft David Desktop");
            coPilot.SetOutputToDefaultAudioDevice();

            crew = new SpeechSynthesizer();
            crew.SelectVoice("Microsoft Zira Desktop");
            crew.SetOutputToDefaultAudioDevice();
        }

        public void CoPilot(string msg, bool async = true)
        {
            if (async )
                coPilot.SpeakAsync(msg);
            else
                coPilot.Speak(msg);
        }

        public void Crew(string msg, bool async = true)
        {
            if (async)
                crew.SpeakAsync(msg);
            else
                crew.Speak(msg);
        }

        public void Dispose()
        {
            coPilot.Dispose();
            crew.Dispose();
        }
    }
}
