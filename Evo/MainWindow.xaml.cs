﻿using System.Windows;
using System.Speech.Synthesis;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System;

namespace Evo
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppData appData;
        private String strUser, strFirst;
        private readonly SpeechSynthesizer _speechSynthesizer = new SpeechSynthesizer();

        public MainWindow()
        {
            InitializeComponent();
            loadData();

            strUser = "";
            strFirst = "";
            _speechSynthesizer.SpeakAsync("Welcome to Eevo.");
            listViewEvo.Items.Add("Welcome to Evo.");
            getUsername();
        }

        private void saveData()
        {
            FileStream fs = new FileStream("udata.dat", FileMode.Create);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, appData);
            }
            catch (SerializationException ec)
            {
                MessageBox.Show(ec.Message, "Speicherfehler", MessageBoxButton.OK);
                //Console.WriteLine("Failed to serialize. Reason: " + ec.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        private void loadData()
        {
            IFormatter formatter = new BinaryFormatter();
            try
            {
                Stream stream = new FileStream("udata.dat", FileMode.Open, FileAccess.Read, FileShare.Read);
                appData = (AppData)formatter.Deserialize(stream);
                stream.Close();
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.Message, "Dateifehler", MessageBoxButton.OK);
                appData = new AppData();
                //Application.Current.Shutdown();
                //throw;
            }
        }

        private void getStatus()
        {
            _speechSynthesizer.SpeakAsync("I have the following status:");
            listViewEvo.Items.Add("I have the following status:");
            _speechSynthesizer.SpeakAsync("My name is Eevo.");
            listViewEvo.Items.Add("My name is " + appData.strName + ".");
            _speechSynthesizer.SpeakAsync("My database has a size of " + fileSize() + ".");
            listViewEvo.Items.Add("My database has a size of " + fileSize()+ ".");
            _speechSynthesizer.SpeakAsync("My vocabulary includes " + appData.listNouns.Count.ToString() + " words.");
            listViewEvo.Items.Add("My vocabulary includes " + appData.listNouns.Count.ToString() + " words.");

            if (appData.listNouns.Count < 1000)
            {
                _speechSynthesizer.SpeakAsync("It looks like I have a lot to learn.");
                listViewEvo.Items.Add("It looks like I have a lot to learn.");
            }
        }

        private void getUsername()
        {
            _speechSynthesizer.SpeakAsync("Now, please enter your first and last name. I need it to distinguish you from other persons.");
            listViewEvo.Items.Add("Now, please enter your first- and lastname.\nI need it to distinguish you from other persons.");

            textBoxUser.Focus();
            textBoxUser.SelectAll();
        }

        private String fileSize()
        {
            String strFileName = "udata.dat";
            FileInfo f = new FileInfo(strFileName);
            long lKBs = f.Length;
            return lKBs.ToString() + " Bytes";
        }

        private Boolean checkEntry(String strTemp)
        {
            foreach (Noun item in appData.listNouns)
            {
                if (item.strName.Equals(strTemp))
                {
                    _speechSynthesizer.SpeakAsync("I already know this word.");
                    listViewEvo.Items.Add("I already know this word.");
                    return true;
                }
            }
            _speechSynthesizer.SpeakAsync("This is a new word. I will save it.");
            listViewEvo.Items.Add("This is a new word. I will save it.");
            return false;
        }

        private void buttonSubmit_Click(object sender, RoutedEventArgs e)
        {
            listViewUser.Items.Add(textBoxUser.Text);

            if (strUser.Equals(""))
            {
                if (textBoxUser.Text.Trim().Length < 1)
                {
                    _speechSynthesizer.SpeakAsync("You have not entered a valid name. Please try again.");
                    listViewEvo.Items.Add("You have not entered a valid name. Please try again.");
                }
                else
                {
                    strUser = textBoxUser.Text.Trim();
                    strFirst = strUser.Split(' ')[0];
                    saveData();
                    _speechSynthesizer.SpeakAsync("Thank you. Your Name is " + textBoxUser.Text.Trim() + ".");
                    listViewEvo.Items.Add("Thank you. Your Name is " + textBoxUser.Text.Trim() + ".");
                    _speechSynthesizer.SpeakAsync("To make things easier, I will call you " + strFirst + ".");
                    listViewEvo.Items.Add("To make things easier I will call you " + strFirst + ".");
                    getStatus();
                }
            }
            else
            {
                _speechSynthesizer.SpeakAsync("You have entered the word: " + textBoxUser.Text + ".\n" +
                    "Now I Will check, if I already know this word.");
                listViewEvo.Items.Add("You have entered the word: " + textBoxUser.Text + ".\n" +
                    "Now I Will check, if I already know this word.");

                checkEntry(textBoxUser.Text);
            }
        }

        private void buttonTraining_Click(object sender, RoutedEventArgs e)
        {
            _speechSynthesizer.SpeakAsync("OK, lets do some training. Please enter a noun that i will learn.");
            listViewEvo.Items.Add("OK, let´s do some training. Please enter a noun that i will learn.");
        }

        private void buttonStatus_Click(object sender, RoutedEventArgs e)
        {
            getStatus();
        }
    }
}
