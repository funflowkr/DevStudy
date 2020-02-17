﻿using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class JsonParseComponent : MonoBehaviour
{
	/// <summary>
	/// {"Powers":[{"Type":5,"Value":5127},{"Type":1,"Value":285},{"Type":3,"Value":96},{"Type":10,"Value":5.4}]}
	/// </summary>
	public string inputString = "";

	[Serializable]
	public class CollectionPower
	{
		public class CollectionPowerStat
		{
			public byte Type;
			public decimal Value;

			public CollectionPowerStat Clone()
			{
				return new CollectionPowerStat { Type = this.Type, Value = this.Value };
			}
		}

		public List<CollectionPowerStat> Powers;
	}

	public static T JsonToObject<T>(string json)
	{
		return LitJson.JsonMapper.ToObject<T>(json);
	}

	// Start is called before the first frame update
	void Start()
    {
		CollectionPower powers = JsonToObject<CollectionPower>(inputString);

		string result = string.Empty;
		powers.Powers.ForEach(_ => result +=string.Format("{0} : {1}\n", _.Type, _.Value));

		TextManager.Instance.Set(result);

	}

}