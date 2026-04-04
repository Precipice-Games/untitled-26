namespace Yarn.Unity.Variables {

    using Yarn.Unity;

    [System.CodeDom.Compiler.GeneratedCode("YarnSpinner", "3.1.3.0")]
    public partial class YarnVariables : Yarn.Unity.InMemoryVariableStorage, Yarn.Unity.IGeneratedVariableStorage {

        // Accessor for Bool $capsley_likes_you
        /// <summary>
        /// Whether Capsley like you or not. This starts true, but may change.
        /// </summary>
        public bool CapsleyLikesYou {
            get => this.GetValueOrDefault<bool>("$capsley_likes_you");
            set => this.SetValue<bool>("$capsley_likes_you", value);
        }

        // Accessor for String $player_name
        /// <summary>
        /// The player's name. The player chooses this. It starts empty.
        /// </summary>
        public string PlayerName {
            get => this.GetValueOrDefault<string>("$player_name");
            set => this.SetValue<string>("$player_name", value);
        }

        // Accessor for Bool $iceFinished
        /// <summary>
        /// Whether the Ice Island has been completed or not. this will be changed through an external method in IslandManager.cs using the method
        /// variableStorage.SetValue("$iceFinished", true);
        /// </summary>
        public bool IceFinished {
            get => this.GetValueOrDefault<bool>("$iceFinished");
            set => this.SetValue<bool>("$iceFinished", value);
        }

    }
}
