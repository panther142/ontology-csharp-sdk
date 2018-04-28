using Newtonsoft.Json.Linq;

namespace Common.Models
{
    public static class abiModels
    {
        private static readonly string idContract = @"{
    ""hash"": ""80b0cc71bda8653599c5666cae084bff587e2de1"",
        ""entrypoint"": ""Main"",
            ""functions"":
    [
        {
            ""name"": ""Main"",
            ""parameters"":
                [
                    {
                        ""name"": ""operation"",
                        ""type"": ""String""
                    },
                    {
                        ""name"": ""args"",
                        ""type"": ""Array""
                    }
                ],
            ""returntype"": ""Any""
        },
        {
            ""name"": ""RegIdWithPublicKey"",
            ""parameters"":
                [
                    {
                        ""name"": ""ontId"",
                        ""type"": ""ByteArray""
                    },
                    {
                        ""name"": ""publicKey"",
                        ""type"": ""ByteArray""
                    }
                ],
            ""returntype"": ""Boolean""
        },
        {
            ""name"": ""RegIdWithAttributes"",
            ""parameters"":
                [
                    {
                        ""name"": ""ontId"",
                        ""type"": ""ByteArray""
                    },
                    {
                        ""name"": ""publicKey"",
                        ""type"": ""ByteArray""
                    },
                    {
                        ""name"": ""tuples"",
                        ""type"": ""ByteArray""
                    }
                ],
            ""returntype"": ""Boolean""
        },
        {
            ""name"": ""AddKey"",
            ""parameters"":
                [
                    {
                        ""name"": ""ontId"",
                        ""type"": ""ByteArray""
                    },
                    {
                        ""name"": ""newPublicKey"",
                        ""type"": ""ByteArray""
                    },
                    {
                        ""name"": ""sender"",
                        ""type"": ""ByteArray""
                    }
                ],
            ""returntype"": ""Boolean""
        },
        {
            ""name"": ""RemoveKey"",
            ""parameters"":
                [
                    {
                        ""name"": ""ontId"",
                        ""type"": ""ByteArray""
                    },
                    {
                        ""name"": ""oldPublicKey"",
                        ""type"": ""ByteArray""
                    },
                    {
                        ""name"": ""sender"",
                        ""type"": ""ByteArray""
                    }
                ],
            ""returntype"": ""Boolean""
        },
        {
            ""name"": ""AddRecovery"",
            ""parameters"":
                [
                    {
                        ""name"": ""ontId"",
                        ""type"": ""ByteArray""
                    },
                    {
                        ""name"": ""recovery"",
                        ""type"": ""ByteArray""
                    },
                    {
                        ""name"": ""publicKey"",
                        ""type"": ""ByteArray""
                    }
                ],
            ""returntype"": ""Boolean""
        },
        {
            ""name"": ""ChangeRecovery"",
            ""parameters"":
                [
                    {
                        ""name"": ""ontId"",
                        ""type"": ""ByteArray""
                    },
                    {
                        ""name"": ""newRecovery"",
                        ""type"": ""ByteArray""
                    },
                    {
                        ""name"": ""recovery"",
                        ""type"": ""ByteArray""
                    }
                ],
            ""returntype"": ""Boolean""
        },
        {
            ""name"": ""AddAttribute"",
            ""parameters"":
                [
                    {
                        ""name"": ""ontId"",
                        ""type"": ""ByteArray""
                    },
                    {
                        ""name"": ""path"",
                        ""type"": ""ByteArray""
                    },
                    {
                        ""name"": ""type"",
                        ""type"": ""ByteArray""
                    },
                    {
                        ""name"": ""value"",
                        ""type"": ""ByteArray""
                    },
                    {
                        ""name"": ""publicKey"",
                        ""type"": ""ByteArray""
                    }
                ],
            ""returntype"": ""Boolean""
        },
        {
            ""name"": ""RemoveAttribute"",
            ""parameters"":
                [
                    {
                        ""name"": ""ontId"",
                        ""type"": ""ByteArray""
                    },
                    {
                        ""name"": ""path"",
                        ""type"": ""ByteArray""
                    },
                    {
                        ""name"": ""publicKey"",
                        ""type"": ""ByteArray""
                    }
                ],
            ""returntype"": ""Boolean""
        },
        {
            ""name"": ""GetPublicKeys"",
            ""parameters"":
                [
                    {
                        ""name"": ""ontId"",
                        ""type"": ""ByteArray""
                    }
                ],
            ""returntype"": ""ByteArray""
        },
        {
            ""name"": ""GetAttributes"",
            ""parameters"":
                [
                    {
                        ""name"": ""ontId"",
                        ""type"": ""ByteArray""
                    }
                ],
            ""returntype"": ""ByteArray""
        },
        {
            ""name"": ""GetDDO"",
            ""parameters"":
                [
                    {
                        ""name"": ""id"",
                        ""type"": ""ByteArray""
                    },
                    {
                        ""name"": ""nonce"",
                        ""type"": ""ByteArray""
                    }
                ],
            ""returntype"": ""ByteArray""
        }
    ],
        ""events"":
    [
        {
            ""name"": ""Register"",
            ""parameters"":
                [
                    {
                        ""name"": ""op"",
                        ""type"": ""String""
                    },
                    {
                        ""name"": ""ontId"",
                        ""type"": ""ByteArray""
                    }
                ],
            ""returntype"": ""Void""
        },
        {
            ""name"": ""PublicKey"",
            ""parameters"":
                [
                    {
                        ""name"": ""op"",
                        ""type"": ""String""
                    },
                    {
                        ""name"": ""ontId"",
                        ""type"": ""ByteArray""
                    },
                    {
                        ""name"": ""publicKey"",
                        ""type"": ""ByteArray""
                    }
                ],
            ""returntype"": ""Void""
        },
        {
            ""name"": ""Attribute"",
            ""parameters"":
                [
                    {
                        ""name"": ""op"",
                        ""type"": ""String""
                    },
                    {
                        ""name"": ""ontId"",
                        ""type"": ""ByteArray""
                    },
                    {
                        ""name"": ""attrName"",
                        ""type"": ""ByteArray""
                    }
                ],
            ""returntype"": ""Void""
        },
        {
            ""name"": ""Debug"",
            ""parameters"":
                [
                    {
                        ""name"": ""func"",
                        ""type"": ""String""
                    },
                    {
                        ""name"": ""info"",
                        ""type"": ""ByteArray""
                    }
                ],
            ""returntype"": ""Void""
        },
        {
            ""name"": ""Debug"",
            ""parameters"":
                [
                    {
                        ""name"": ""func"",
                        ""type"": ""String""
                    },
                    {
                        ""name"": ""trace"",
                        ""type"": ""Integer""
                    }
                ],
            ""returntype"": ""Void""
        }
    ]
}";


        public static JToken GetFunction(string name)
        {
            JToken t = null;

            JToken t1 = JToken.Parse(idContract);
            if (t1 != null)
            {
                var functions = JArray.Parse(t1["functions"].ToString());

                foreach (var tokenFunction in functions)
                {
                    if (tokenFunction["name"].ToString() == name)
                    {
                        return tokenFunction;
                    }
                }
            }


            return t;
        }

        public static string GetHash()
        {
            string hash = "";

            JToken t1 = JToken.Parse(idContract);
            if (t1 != null)
            {
                hash = t1["hash"].ToString();
            }
            return hash;
        }
    }

    public class abiParameter
    {
        public string name { get; set; }
        public string type { get; set; }
    }


}
