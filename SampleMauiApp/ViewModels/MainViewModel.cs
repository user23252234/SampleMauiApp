using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace SampleMauiApp.ViewModels
{ 
    public partial class MainViewModel : ObservableObject
    {

        public class QuestionModel
        {
            public string QuestionContent { get; set; } = "";
        }

        [ObservableProperty]
        private ObservableCollection<QuestionModel> _questions = new();

        public MainViewModel()
        {
            Questions.Add(new QuestionModel() { QuestionContent = "Ques No.1"});
            Questions.Add(new QuestionModel() { QuestionContent = "Ques No.2" });
            Questions.Add(new QuestionModel() { QuestionContent = "Ques No.3" });
            Questions.Add(new QuestionModel() { QuestionContent = "Ques No.4" });
            Questions.Add(new QuestionModel() { QuestionContent = "Ques No.5" });
        }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(NextQuestionCommand))]
        [NotifyCanExecuteChangedFor(nameof(PreviousQuestionCommand))]
        private int _carouselViewPosition = 0;

        partial void OnCarouselViewPositionChanged(int oldValue, int newValue)
        {
            var stackFrame = new StackFrame(1);
            var method = stackFrame.GetMethod();
            Debug.WriteLine($"[set_CarouselViewPosition] Set method called from {method.Name}");
        }

        [RelayCommand(CanExecute = nameof(CanGoNextQuestion))]
        private void OnNextQuestion()
        {
            Debug.WriteLine($"[OnNextQuestion] About to increment the position in carousel: {CarouselViewPosition}");
            CarouselViewPosition += 1;
            Debug.WriteLine($"[OnNextQuestion] Incremented position in carousel: {CarouselViewPosition}");
        }

        [RelayCommand(CanExecute = nameof(CanGoPreviousQuestion))]
        private void OnPreviousQuestion()
        {
            Debug.WriteLine($"[OnPreviousQuestion] About to decrement the position in carousel: {CarouselViewPosition}");
            CarouselViewPosition -= 1;
            Debug.WriteLine($"[OnPreviousQuestion] Decremented position in carousel: {CarouselViewPosition}");
        }

        private bool CanGoNextQuestion()
        {
            Debug.WriteLine($"[CanGoNextQuestion] Position in carousel: {CarouselViewPosition}");
            if (CarouselViewPosition < (Questions.Count() - 1))
                return true;
            else
                return false;
        }

        private bool CanGoPreviousQuestion()
        {
            Debug.WriteLine($"[CanGoPreviousQuestion] Position in carousel: {CarouselViewPosition}");
            if (CarouselViewPosition > 0)
                return true;
            else
                return false;
        }
    }
}
