# Endless Runner Scripting API

# Ability

## Namespace
EndlessRunner.Abilities

## Description
Represents an ability that can be used by a character in the game. The ability can target an enemy or ally, apply effects, and manage cooldowns and mana costs.

## Inheritance
Inherits from `InventoryItem`.

### Private Fields
- `TargetingStrategy targeting`: Strategy used for targeting.
- `EffectStrategy[] effects`: Array of effects applied by the ability.
- `float cooldownTime`: The cooldown duration of the ability.
- `float manaCost`: The mana cost to use the ability.

### Public Methods
- `bool CanUse(CooldownStore cooldownStore, Mana mana)`: Checks if the ability can be used based on the cooldown and available mana.
- `void Use(GameObject user, CooldownStore cooldownStore, Mana mana, Action abilityFinished)`: Activates the ability, starting the targeting process.

### Private Methods
- `void TargetAcquired(AbilityData data, CooldownStore cooldownStore, Mana mana, Action abilityFinished)`: Called when the target is acquired, applies effects, starts cooldown, and consumes mana.

### Usage Example
```csharp
Ability ability = ScriptableObject.CreateInstance<Ability>();
CooldownStore cooldownStore = new CooldownStore();
Mana mana = new Mana();

if (ability.CanUse(cooldownStore, mana))
{
    ability.Use(gameObject, cooldownStore, mana, () => { 
        Debug.Log("Ability finished!"); 
    });
}
```

# AbilityData

## Namespace
EndlessRunner.Abilities

## Description
Stores information about the user and target of an ability. This class provides methods to set and retrieve the user and target GameObjects.

### Constructors
- `AbilityData(GameObject user)`: Constructor that initializes the user of the ability.

### Private Fields
- `GameObject user`: The GameObject that is using the ability.
- `GameObject target`: The GameObject that is the target of the ability.

### Public Methods
- `GameObject GetUser()`: Retrieves the user GameObject.
- `GameObject GetTarget()`: Retrieves the target GameObject.
- `void SetTarget(GameObject target)`: Sets the target GameObject.

### Usage Example
```csharp
GameObject user = new GameObject();
AbilityData abilityData = new AbilityData(user);

GameObject target = new GameObject();
abilityData.SetTarget(target);

Debug.Log($"User: {abilityData.GetUser().name}, Target: {abilityData.GetTarget().name}");
```

# AbilityStore

## Namespace
EndlessRunner.Abilities

## Description
Manages a collection of abilities that can be used by the character. This class handles ability usage, cooldowns, and mana consumption, as well as evaluating predicates for action conditions.

### Private Fields
- `Ability[] abilities`: Array of abilities available in the store.
- `Ability currentAbility`: The currently active ability.
- `CooldownStore cooldownStore`: Reference to the CooldownStore component.
- `Mana mana`: Reference to the Mana component.

### Public Methods
- `Ability GetAbility(int index)`: Retrieves the ability at the specified index.

### Private Methods
- `void Awake()`: Initializes references to the CooldownStore and Mana components.
- `void Use(int index)`: Uses the ability at the specified index.
- `void AbilityFinished()`: Resets the current ability to null when it finishes.
- `void IAction.DoAction(string actionID, string[] parameters)`: Executes an action based on the action ID.
- `bool? IPredicateEvaluator.Evaluate(string predicate, string[] parameters)`: Evaluates predicates related to ability usage.

### Usage Example
```csharp
AbilityStore abilityStore = GetComponent<AbilityStore>();
Ability ability = abilityStore.GetAbility(0);

if (ability.CanUse(cooldownStore, mana))
{
    abilityStore.DoAction("Use Ability", new string[] { "0" });
}
```
# EffectStrategy

## Namespace
EndlessRunner.Abilities

## Description
An abstract base class for defining different strategies for applying effects associated with abilities. This class allows for the implementation of various effect behaviors.

### Public Methods
- `abstract void StartEffect(AbilityData data, Action finished)`: Abstract method to start the effect with the given ability data and a callback for when the effect finishes.

### Usage Example
```csharp
public class FireEffect : EffectStrategy
{
    public override void StartEffect(AbilityData data, Action finished)
    {
        // Implement fire effect logic here
        finished.Invoke();
    }
}
```

# TargetingStrategy

## Namespace
EndlessRunner.Abilities

## Description
An abstract base class for defining different strategies for targeting in abilities. This class enables the implementation of various targeting behaviors.


### Public Methods
- `abstract void StartTargeting(AbilityData data, Action finished)`: Abstract method to start the targeting process with the given ability data and a callback for when targeting is finished.

### Usage Example
```csharp
public class SingleTargetStrategy : TargetingStrategy
{
    public override void StartTargeting(AbilityData data, Action finished)
    {
        // Implement single target targeting logic here
        finished.Invoke();
    }
}
```
# HealthEffect

## Namespace
EndlessRunner.Abilities.Effects

## Description
An effect strategy that applies health modifications to a target, either healing or dealing damage based on the specified health points.

### Private Fields
- `int healthPoints`: The amount of health to be added or subtracted from the target.

### Public Methods
- `override void StartEffect(AbilityData data, Action finished)`: Applies the health effect to the user based on the healthPoints value, invoking the finished callback once done.

### Usage Example
```csharp
HealthEffect healthEffect = ScriptableObject.CreateInstance<HealthEffect>();
healthEffect.healthPoints = -10; // Damage

AbilityData abilityData = new AbilityData(user);
healthEffect.StartEffect(abilityData, () => { 
    Debug.Log("Health effect applied!"); 
});
```

# SpawnPrefabEffect

## Namespace
EndlessRunner.Abilities.Effects

## Description
An effect strategy that spawns a specified prefab at the location of the ability's user when the effect is triggered.

### Private Fields
- `GameObject prefab`: The prefab to be instantiated when the effect is activated.

### Public Methods
- `override void StartEffect(AbilityData data, Action finished)`: Instantiates the prefab at the user's position and invokes the finished callback.

### Usage Example
```csharp
SpawnPrefabEffect spawnEffect = ScriptableObject.CreateInstance<SpawnPrefabEffect>();
spawnEffect.prefab = somePrefab; // Assign a prefab to spawn

AbilityData abilityData = new AbilityData(user);
spawnEffect.StartEffect(abilityData, () => { 
    Debug.Log("Prefab spawned!"); 
});
```

# SelfTargeting

## Namespace
EndlessRunner.Abilities.Targeting

## Description
A targeting strategy that targets the user themselves. This is typically used for abilities that have effects only on the caster.

### Public Methods
- `override void StartTargeting(AbilityData data, Action finished)`: Sets the target of the ability data to the user and invokes the finished callback.

### Usage Example
```csharp
SelfTargeting selfTargeting = ScriptableObject.CreateInstance<SelfTargeting>();

AbilityData abilityData = new AbilityData(user);
selfTargeting.StartTargeting(abilityData, () => { 
    Debug.Log("Target set to self!"); 
});
```

# Health

## Namespace
EndlessRunner.Attributes

## Description
Manages the health of a game object, allowing it to take damage, heal, and trigger events on death or health changes.

### Constructors
*No constructors available.*

### Private Fields
- `int maxHealthPoints`: The maximum health points the object can have.
- `int currentHealthPoints`: The current health points of the object.

### Public Methods
- `int GetMaxHealthPoints()`: Returns the maximum health points.
- `int GetCurrentHealthPoints()`: Returns the current health points.
- `void TakeDamage(int damagePoints)`: Reduces the current health points by the specified damage points, invoking the appropriate events.
- `void Heal(int healPoints)`: Increases the current health points by the specified heal points, invoking the healing event if applicable.
- `void Kill()`: Instantly reduces the current health points to zero.

### Private Methods
- `void Awake()`: Initializes the current health points to the maximum at the start.
- `bool IsDead()`: Checks if the current health points are less than or equal to zero.
- `bool HasFullHealth()`: Checks if the current health points are equal to the maximum.

### Usage Example
```csharp
Health healthComponent = gameObject.AddComponent<Health>();
healthComponent.TakeDamage(1);
healthComponent.Heal(1);
if (healthComponent.GetCurrentHealthPoints() <= 0)
{
    Debug.Log("Character has died!");
}
```

# Mana

## Namespace
EndlessRunner.Attributes

## Description
Manages the mana for a game object, including its regeneration and usage for abilities.

### Private Fields
- `float maxMana`: The maximum amount of mana the object can have.
- `float regenRate`: The rate at which mana regenerates over time.
- `float currentMana`: The current amount of mana available.

### Public Methods
- `float GetManaFraction()`: Returns the fraction of current mana relative to the maximum mana.
- `bool CanUse(float manaToUse)`: Checks if there is enough current mana to use the specified amount.
- `void Use(float manaToUse)`: Reduces the current mana by the specified amount if enough mana is available.

### Private Methods
- `void Awake()`: Initializes the current mana to the maximum at the start.
- `void Update()`: Regenerates mana over time based on the regen rate, ensuring it does not exceed the maximum.

### Usage Example
```csharp
Mana manaComponent = gameObject.AddComponent<Mana>();
if (manaComponent.CanUse(5))
{
    manaComponent.Use(5);
    Debug.Log("Mana used!");
}
Debug.Log("Current mana fraction: " + manaComponent.GetManaFraction());
```

# Killer

## Namespace
EndlessRunner.Combat

## Description
Detects collisions with other game objects and kills them if they have a `Health` component.

### Private Methods
- `void OnTriggerEnter(Collider other)`: Triggered when a collider enters the trigger area; if the other object has a `Health` component, it calls the `Kill()` method on that component.

### Usage Example
```csharp
// This component can be added to any GameObject that should kill others on trigger.
Killer killerComponent = gameObject.AddComponent<Killer>();
```

# Projectile

## Namespace
EndlessRunner.Combat

## Description
Represents a projectile that can move in a specified direction, apply effects on collision, and optionally be destroyed after hitting a target.

### Private Fields
- `TriggerEffect effect`: The effect that the projectile applies upon collision (e.g., damage, currency, or none).
- `Vector2 direction`: The direction in which the projectile travels.
- `int effectPoints`: The amount of effect points applied when the projectile hits a target.
- `float speed`: The speed at which the projectile moves.
- `bool destroyAfterHit`: Indicates whether the projectile should be destroyed after hitting a target.
- `bool useGameSpeed`: Indicates whether the projectile speed should be affected by the game's speed.
- `GameSettings gameSettings`: Reference to the game's settings.
- `GameObject user`: The game object that fired the projectile.

### Public Methods
- `void SetData(GameObject user, Vector2 direction)`: Sets the user and direction for the projectile.

### Private Methods
- `void Awake()`: Initializes the game settings reference.
- `void Update()`: Updates the projectile's position based on its speed and direction.
- `void OnBecameInvisible()`: Destroys the projectile when it goes off-screen.
- `void OnTriggerEnter(Collider other)`: Handles the collision with other game objects and applies effects based on the `TriggerEffect`.
- `void DoDamage(Collider other)`: Applies damage to a `Health` component on collision.
- `void Deposit(Collider other)`: Deposits effect points into a `Purse` component on collision.

### Usage Example
```csharp
Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
projectile.SetData(gameObject, transform.right);
```
# InputReader

## Namespace
EndlessRunner.Control

## Description
Handles player input through the Unity Input System and evaluates input predicates for actions like pressing, releasing, and holding buttons.

### Private Fields
- `PlayerInput playerInput`: Reference to the `PlayerInput` component managing player input actions.

### Private Methods
- `void Awake()`: Initializes the `PlayerInput` reference.
- `InputAction GetInputAction(string actionName)`: Retrieves the `InputAction` corresponding to the specified action name.
- `bool? IPredicateEvaluator.Evaluate(string predicate, string[] parameters)`: Evaluates input predicates based on the provided parameters, checking if actions are pressed, released, or held.

### Usage Example
```csharp
InputReader inputReader = GetComponent<InputReader>();

if (inputReader.Evaluate("Input Action Pressed", new string[] { "Jump" }) == true)
{
    // Execute jump action
}
```
# AnimationPlayer

## Namespace
EndlessRunner.Core

## Description
Manages the playback of animations using an `Animator` component, allowing for smooth transitions between animations.

### Private Fields
- `float transitionTime`: The duration of the transition between animations.
- `Animator animator`: Reference to the `Animator` component used for animation playback.

### Private Methods
- `void Awake()`: Initializes the `Animator` reference.
- `void IAction.DoAction(string actionID, string[] parameters)`: Executes actions based on the provided action ID, such as playing a specified animation.

### Usage Example
```csharp
AnimationPlayer animationPlayer = GetComponent<AnimationPlayer>();
animationPlayer.DoAction("Play Animation", new string[] { "Jump" });
```

# CooldownStore

## Namespace
EndlessRunner.Core

## Description
Manages cooldown timers for various abilities or actions, allowing tracking of time remaining and cooldown fractions.

### Private Fields
- `Dictionary<object, float> cooldownTimers`: Stores the current cooldown times for different targets.
- `Dictionary<object, float> initialCooldownTimers`: Stores the initial cooldown times for the targets.

### Public Methods
- `void StartCooldown(object target, float cooldownTime)`: Initiates a cooldown for a specified target with the given duration.
- `float GetTimeRemaining(object target)`: Retrieves the time remaining on the cooldown for the specified target.
- `float GetFractionRemaining(object target)`: Returns the fraction of the cooldown time that remains for the specified target.

### Private Methods
- `void Update()`: Updates the cooldown timers every frame, removing any targets that have completed their cooldown.

### Usage Example
```csharp
CooldownStore cooldownStore = GetComponent<CooldownStore>();
cooldownStore.StartCooldown("Ability1", 5f);

float timeRemaining = cooldownStore.GetTimeRemaining("Ability1");
float fractionRemaining = cooldownStore.GetFractionRemaining("Ability1");
```
# ForceReceiver

## Namespace
EndlessRunner.Core

## Description
Handles vertical movement mechanics, including jumping and gravity application for a character controlled by a `CharacterController`.

### Private Fields
- `float jumpForce`: The force applied when the character jumps.
- `float jumpTime`: The duration for which the jump is active.
- `float gravityMultiplier`: A multiplier to increase the effect of gravity.
- `float edgeHitDuration`: The duration for which the edge hit state is active.
- `CharacterController controller`: Reference to the `CharacterController` component for movement.
- `float verticalVelocity`: The current vertical velocity of the character.
- `float timeSinceJumped`: Tracks the time since the last jump was initiated.
- `bool edgeHit`: Indicates if the character is currently in an edge hit state.

### Private Methods
- `void Awake()`: Initializes the `CharacterController` component reference.
- `void Update()`: Updates the vertical velocity and applies gravity each frame.
- `void ApplyGravity()`: Applies gravity to the character, managing the vertical velocity based on ground state.
- `bool JumpTimeFinished()`: Checks if the jump time has elapsed.
- `void Jump()`: Sets the vertical velocity to the jump force.
- `void OnTriggerEnter(Collider other)`: Detects collision with an edge and starts the edge hit routine.
- `IEnumerator EdgeHitRoutine()`: Manages the edge hit state duration.
- `void IAction.DoAction(string actionID, string[] parameters)`: Executes actions based on the action ID:
  - `"Start Jump"`: Resets the jump timer.
  - `"Jump"`: Initiates the jump.
  
- `bool? IPredicateEvaluator.Evaluate(string predicate, string[] parameters)`: Evaluates conditions based on predicates:
  - `"Is Grounded"`: Returns true if the character is grounded and not in an edge hit state.
  - `"Jump Time Finished"`: Returns true if the jump time has elapsed.

### Usage Example
```csharp
ForceReceiver forceReceiver = GetComponent<ForceReceiver>();
forceReceiver.DoAction("Start Jump");
if (forceReceiver.Evaluate("Is Grounded", null) == true)
{
    forceReceiver.DoAction("Jump");
}
```

# ParallaxEffect
## Namespace
EndlessRunner.Core

## Description
The `ParallaxEffect` class creates a parallax scrolling effect for multiple background items based on the game speed. Each item moves at a different speed to create a sense of depth in the scene.


### Private Fields
- `ParallaxItem[] parallaxItems`: An array of parallax items to be affected by the scrolling effect.
- `GameSettings settings`: A reference to the `GameSettings` to access the current game speed.

### Private Methods
- `void Awake()`: Initializes the `settings` variable by finding the `GameSettings` object in the scene.
- `void Update()`: Updates the texture offset of each parallax item based on the current game speed and their individual speed multipliers, creating the parallax scrolling effect.

### Structs
- `struct ParallaxItem`
  - `MeshRenderer renderer`: The renderer for the parallax item.
  - `float speedMultiplier`: The multiplier for the speed of the parallax effect for this item.

# PlatformSpawner

## Namespace
EndlessRunner.Core

## Description
The `PlatformSpawner` class is responsible for spawning platforms in a sequence as the player progresses through the game. It determines when to spawn the next platform based on the distance from the current platform.

### Private Fields
- `PlatformConfig initialPlatform`: The configuration for the initial platform to be spawned.
- `PlatformConfig[] platformSequence`: An array of platform configurations that can be spawned in sequence.
- `PlatformConfig currentConfig`: The current platform configuration being used.
- `GameObject currentPlatform`: The currently spawned platform instance.

### Private Methods
- `void Start()`: Initializes the platform spawner by setting the current platform configuration to the initial platform and referencing the initial platform's GameObject.
- `void Update()`: Checks the distance from the player to the current platform and spawns a new platform if the required spawn distance is reached.

### Structs
- `PlatformConfig`
  - **Fields**:
    - `GameObject platform`: The GameObject to be instantiated as a platform.
    - `float spawnDistance`: The distance required from the current platform to spawn the next platform.

# Scorer

## Namespace
EndlessRunner.Core

## Description
The `Scorer` class keeps track of the player's score in the game. The score increases over time based on the game speed, rewarding players for progressing.


### Private Fields
- `GameSettings settings`: Reference to the `GameSettings` instance for accessing the current game speed.
- `float currentScore`: The player's current score.

### Public Methods
- `float GetScore()`: Returns the current score of the player.

### Private Methods
- `void Awake()`: Initializes the `Scorer` by retrieving the `GameSettings` instance to access the game speed.
- `void Update()`: Increments the current score based on the game speed each frame.

### Usage Example
```csharp
Scorer scorer = GetComponent<Scorer>();
float score = scorer.GetScore();
```

# SpriteColorSwapper

## Namespace
`EndlessRunner.Core`

## Description
A utility class that temporarily swaps the color of a sprite to a specified color, defined by an HTML color code, for a set duration. After the duration expires, it reverts to the original color.

### Private Fields
- `SpriteRenderer spriteRenderer` - Reference to the sprite renderer component.
- `Color initialColor` - Stores the initial color of the sprite for resetting after the swap.

### Public Methods
- `void SwapColor(string colorHtml)` - Starts the color-swapping coroutine to change the sprite's color to the specified color in HTML format for the configured duration.

### Private Methods
- `void Awake()` - Initializes the `spriteRenderer` and saves the initial color of the sprite when the object awakens.
- `IEnumerator SwapColorRoutine(string colorHtml)` - Coroutine that attempts to parse the HTML color, sets it to the sprite, waits for the duration, and then reverts to the initial color. Logs an error if the color parsing fails.

### Usage Example
```csharp
SpriteColorSwapper colorSwapper = GetComponent<SpriteColorSwapper>();
colorSwapper.SwapColor("FF5733"); // Changes sprite color temporarily to #FF5733
