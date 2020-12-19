using System.Linq;
using Elsa.ActivityResults;
using Elsa.Builders;
using Elsa.Services.Models;

namespace Elsa.Services
{
    public class CompositeActivity : Activity
    {
        public virtual void Build(ICompositeActivityBuilder composite)
        {
        }

        private bool IsScheduled
        {
            get => GetState<bool>();
            set => SetState(value);
        }

        protected override IActivityExecutionResult OnExecute(ActivityExecutionContext context)
        {
            var workflowInstance = context.WorkflowExecutionContext.WorkflowInstance;
            
            if (IsScheduled)
            {
                if(workflowInstance.ParentActivities.Any())
                    workflowInstance.ParentActivities.Pop();
                return Complete(context);
            }
            
            var compositeActivityBlueprint = (ICompositeActivityBlueprint)context.ActivityBlueprint;
            var startActivities = compositeActivityBlueprint.GetStartActivities().Select(x => x.Id).ToList();
            workflowInstance.ParentActivities.Push(Id);
            context.WorkflowExecutionContext.PostScheduleActivity(Id);
            IsScheduled = true;
            return Schedule(startActivities, context.Input);
        }

        protected virtual IActivityExecutionResult Complete(ActivityExecutionContext context) => Done();
    }
}