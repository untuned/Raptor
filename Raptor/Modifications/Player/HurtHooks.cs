using System.Reflection;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Raptor.Hooks;

namespace Raptor.Modifications.Player
{
    using static Instruction;

    internal sealed class HurtHooks : Modification
    {
        private const BindingFlags Flags = BindingFlags.NonPublic | BindingFlags.Static;

        public override void Apply(AssemblyDefinition assembly)
        {
            var player = assembly.GetType("Player");
            var module = player.Module;
            var hurt = player.GetMethod("Hurt");
            hurt.InjectBeginning(
                Create(OpCodes.Ldarg_0),
                Create(OpCodes.Ldarg_1),
                Create(OpCodes.Ldarg_2),
                Create(OpCodes.Ldarg_S, hurt.Parameters[3]), 
                Create(OpCodes.Ldarg_S, hurt.Parameters[5]), 
                Create(OpCodes.Call, module.Import(typeof(PlayerHooks).GetMethod("InvokeHurting", Flags))),
                Create(OpCodes.Brfalse_S, hurt.Body.Instructions[0]),
                Create(OpCodes.Ldc_R8, 0.0),
                Create(OpCodes.Ret));
            hurt.InjectEndings(
                Create(OpCodes.Ldarg_0),
                Create(OpCodes.Ldarg_1),
                Create(OpCodes.Ldarg_2),
                Create(OpCodes.Ldarg_S, hurt.Parameters[3]),
                Create(OpCodes.Ldarg_S, hurt.Parameters[5]),
                Create(OpCodes.Call, module.Import(typeof(PlayerHooks).GetMethod("InvokeHurt", Flags))));
            hurt.ReplaceShortBranches();
        }
    }
}
