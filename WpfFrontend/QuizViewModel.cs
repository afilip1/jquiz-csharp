using JQuiz;
using PostSharp.Patterns.Model;
using PostSharp.Patterns.Xaml;
using System;
using System.Windows.Input;

namespace WpfFrontend
{
    public enum QuizState
    {
        Tutorial,
        Question,
        Answer
    }

    [NotifyPropertyChanged]
    public class QuizViewModel
    {
        private readonly KanjiQuiz quiz;

        public QuizState State { get; set; }
        
        public string Guess { get; set; }
        public Word? Question { get; set; }
        public bool AnswerIsCorrect { get; set; }

        public QuizViewModel()
        {
            quiz = new KanjiQuiz();
            Guess = string.Empty;
            State = QuizState.Tutorial;
        }

        [Command]
        public ICommand? SubmitCommand { get; private set; }

        public void ExecuteSubmit()
        {
            if (State != QuizState.Question)
            {
                Question = quiz.GetRandomWord();
                State = QuizState.Question;
            } else
            {
                AnswerIsCorrect = Question?.Readings.Contains(Guess) ?? throw new NullReferenceException("Question was null");
                Guess = string.Empty;
                State = QuizState.Answer;
            }
        }
    }
}
