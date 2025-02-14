﻿using UnityEngine;

namespace FrostweepGames.Plugins.GoogleCloud.SpeechRecognition.V1
{
	[CreateAssetMenu(fileName = "GCSpeechRecognitonConfig", menuName = "Frostweep Games/GCSpeechRecogniton/V1/Config", order = 51)]
	public class Config : ScriptableObject
	{
        public double voiceDetectionThreshold;

		public double voiceDetectionEndTalkingDelay;

		public bool useVolumeMultiplier;

		public float audioVolumeMultiplier;

		public bool enabledAPIKeyValidation;

		public string keySignature;

		public string packageName;

		public Config()
		{
			voiceDetectionThreshold = 0.2;
			voiceDetectionEndTalkingDelay = 1f;
			useVolumeMultiplier = false;
			audioVolumeMultiplier = 1.0f;
			enabledAPIKeyValidation = false;
			keySignature = string.Empty;
			packageName = "com.companyname.appname";
		}
	}
}