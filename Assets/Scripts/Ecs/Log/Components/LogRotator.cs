using System.Collections;
using UnityEngine;
using HitIt.Other;
using HitIt.Storage;

namespace HitIt.Ecs
{
    public class LogRotator : IInizializable
    {
        private LogSettings settings;
        private Coroutine currentOperation;
        private IIterator<LogRotationNode> iterator;
        private bool hasOperation;

        public void Inizialize()
        {
            settings = StorageFacility.Instance.GetStorageByType<LogSettings>(); 
            currentOperation = null;
            hasOperation = false;           
        }

        public void StopOperation()
        {
            GlobalMono.Instance.StopCoroutine(currentOperation);
            hasOperation = false;
        }


        public void SetIterator(IIterator<LogRotationNode> iterator)
        {
            this.iterator = iterator;
        }

        public void Process(LogMono log)
        {
            if(hasOperation == false)
            {               
                ApplyOperation(log);
            }
        }

        private void ApplyOperation(LogMono log)
        {
            LogRotationNode current;

            if (!iterator.HasNext())
            {
                iterator.ToBegin();
            }

            current = iterator.Next();
             
            switch (current.Type)
            {
                case LogRotationType.Acceleration:
                    currentOperation = GlobalMono.Instance.StartCoroutine(AccelerateLog(log, current.Rotation * current.Multiplier, current.Time));
                    break;

                case LogRotationType.Speed:
                    currentOperation = GlobalMono.Instance.StartCoroutine(SetLogVelocity(log, current.Rotation * current.Multiplier, current.Time));
                    break;
            }
        }

        private void StopLog(LogMono log)
        {
            log.Rotatables.angularVelocity = Vector3.zero;
        }

        private IEnumerator AccelerateLog(LogMono log, Vector3 acceleration, float time)
        {
            hasOperation = true;
            log.Rotatables.AddTorque(acceleration, ForceMode.Acceleration); 

            yield return new WaitForSeconds(time);

            hasOperation = false;
            StopLog(log);
        }

        private IEnumerator SetLogVelocity(LogMono log, Vector3 velocity, float time)
        {
            hasOperation = true;
            log.Rotatables.AddTorque(velocity, ForceMode.VelocityChange);

            yield return new WaitForSeconds(time);
           
            hasOperation = false;
            StopLog(log);
        }
    }
}