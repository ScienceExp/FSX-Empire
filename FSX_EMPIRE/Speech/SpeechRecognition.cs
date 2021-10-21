using System;
using System.Speech.Recognition;
using System.Windows.Forms;

namespace FSX_EMPIRE
{
    class SpeechRecognition : IDisposable
    {
        readonly SpeechRecognitionEngine recognizer;
        public SpeechRecognition()
        {
            recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));

            try
            {
                recognizer.SetInputToDefaultAudioDevice();
            }
            catch (Exception)
            {
                MessageBox.Show("No input device found for speech recognition.");
                Dispose(); 
                return;
            }

            GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append(PersonChoices());
            grammarBuilder.Append(SetChoices());
            grammarBuilder.Append(NumberChoices());


            // Create and load a dictation grammar.  
            recognizer.LoadGrammar(new Grammar(grammarBuilder));
            //recognizer.LoadGrammar(new Grammar(new GrammarBuilder(PersonChoices())));
            //recognizer.LoadGrammar(new Grammar(new GrammarBuilder(SetChoices())));
            //recognizer.LoadGrammar(new Grammar(new GrammarBuilder(NumberChoices())));

            // Add a handler for the speech recognized event.  
            recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(SpeechRecognized);
            recognizer.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(SpeechRecognitionRejected);
            // Start asynchronous, continuous speech recognition.  
            recognizer.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            Console.WriteLine(e.Result.Text);
            //G.speechSynth.CoPilot(e.Result.ToString ); 
        }

        Choices PersonChoices()
        {
            Choices c = new Choices();

            c.Add("copilot");
            c.Add("crew");

            return c;
        }

        Choices SetChoices()
        {
            Choices c = new Choices();

            c.Add("set air speed");
            c.Add("set manifold pressure");
            c.Add("set propeller rpm");
            return c;
        }

        Choices NumberChoices()
        {
            Choices c = new Choices();
            for (int i = -1; i < 1000; i++)
            {
                c.Add(i.ToString());

            }
            for (int i = 2000; i < 60000; i+=1000)
            {
                c.Add(i.ToString());

            }
            return c;
        }

        // Handle the SpeechRecognized event.  
        void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            try
            {
                switch (e.Result.Words[0].Text)
                {
                    case "copilot":
                        switch (e.Result.Words[1].Text)
                        {
                            case "set":
                                switch (e.Result.Words[2].Text)
                                {
                                    case "air":
                                        switch (e.Result.Words[3].Text)
                                        {
                                            case "speed":
                                                G.speechSynth.CoPilot("Set air speed " + e.Result.Words[4].Text);
                                                break;
                                        }
                                        break;
                                    case "manifold":
                                        switch (e.Result.Words[3].Text)
                                        {
                                            case "pressure":
                                                G.speechSynth.CoPilot("Set manifold pressure " + e.Result.Words[4].Text);
                                                break;
                                        }
                                        break;
                                    case "propeller":
                                        switch (e.Result.Words[3].Text)
                                        {
                                            case "rpm":
                                                G.speechSynth.CoPilot("Set propeller rpm " + e.Result.Words[4].Text);
                                                break;
                                        }
                                        break;

                                }
                                break;
                        }
                        break;

                    case "crew":
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            Console.WriteLine("Recognized text: " + e.Result.Text);
        }

        public void Dispose()
        {
            if (recognizer != null)
                recognizer.Dispose();
        }
    }
}
