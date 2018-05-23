﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiesAcademy : Academy {

	public float agentRunSpeed;
	public float agentRotationSpeed;
	public float gravityMultiplier;

	public override void InitializeAcademy()
	{
		Physics.gravity *= gravityMultiplier;
	}
}
