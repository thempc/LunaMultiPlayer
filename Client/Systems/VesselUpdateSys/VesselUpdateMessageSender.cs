﻿using System;
using LunaClient.Base;
using LunaClient.Base.Interface;
using LunaClient.Network;
using LunaClient.Systems.Network;
using LunaCommon.Message.Client;
using LunaCommon.Message.Data.Vessel;
using LunaCommon.Message.Interface;
using UnityEngine;

namespace LunaClient.Systems.VesselUpdateSys
{
    public class VesselUpdateMessageSender:SubSystem<VesselUpdateSystem>, IMessageSender
    {
        public void SendMessage(IMessageData msg)
        {
            NetworkSender.QueueOutgoingMessage(MessageFactory.CreateNew<VesselCliMsg>(msg));
        }

        public void SendVesselUpdate(VesselUpdate update)
        {
            var msg = new VesselUpdateMsgData
            {
                GameSentTime = Time.fixedTime,
                Stage = update.Stage,
                PlanetTime = update.PlanetTime,
                ActiveEngines = update.ActiveEngines,
                StoppedEngines = update.StoppedEngines,
                Decouplers = update.Decouplers,
                AnchoredDecouplers = update.AnchoredDecouplers,
                Clamps = update.Clamps,
                Docks = update.Docks,
                VesselId = update.VesselId,
                Acceleration = update.Acceleration,
                ActiongroupControls = update.ActionGrpControls,
                BodyName = update.BodyName,
                GearDown = update.FlightState.gearDown,
                GearUp = update.FlightState.gearUp,
                Headlight = update.FlightState.headlight,
                IsSurfaceUpdate = update.IsSurfaceUpdate,
                KillRot = update.FlightState.killRot,
                MainThrottle = update.FlightState.mainThrottle,
                Orbit = update.Orbit,
                Pitch = update.FlightState.pitch,
                PitchTrim = update.FlightState.pitchTrim,
                Position = update.Position,
                Roll = update.FlightState.roll,
                RollTrim = update.FlightState.rollTrim,
                Rotation = update.Rotation,
                Velocity = update.Velocity,
                WheelSteer = update.FlightState.wheelSteer,
                WheelSteerTrim = update.FlightState.wheelSteerTrim,
                WheelThrottle = update.FlightState.wheelThrottle,
                WheelThrottleTrim = update.FlightState.wheelThrottleTrim,
                X = update.FlightState.X,
                Y = update.FlightState.Y,
                Yaw = update.FlightState.yaw,
                YawTrim = update.FlightState.yawTrim,
                Z = update.FlightState.Z
            };

            SendMessage(msg);
        }
    }
}
