# ChaosHacker



# Config
```yml
chaos_hacker:
  is_enabled: true
  # A chance to spawn a CI Hacker in the next CI spawn wave:
  ci_hacker_spawn_chance: 10
  # Max CI Hackers on the map at the same time. Set to 0 for no limit.
  max_ci_hackers: 1
  # The Chaos Hacker's role name that is shown to other players when they look at him (leave empty to disable custom role name):
  chaos_hacker_role_name: <color=cyan>Chaos Hacker</color>
  # The broadcast shown to Chaos Hacker:
  ci_hacker_broadcast_message: >-
    You are a <color=cyan>Chaos Hacker</color>!

    You have ability to remote access the doors listed in [~]
  # The broadcast duration (in seconds):
  ci_hacker_broadcast_duration: 10
  # The duration of lock door ability:
  ci_hacker_lock_ability_duration: 10
  # The Chaos Hacker ability cooldown (in seconds):
  ci_hacker_ability_cooldown: 100
```
