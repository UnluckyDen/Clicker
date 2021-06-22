[![license](https://img.shields.io/github/license/Leopotam/globals.svg)](https://github.com/Leopotam/globals/blob/develop/LICENSE.md)
# Globals support
[Github repo of globals support](https://github.com/Leopotam/globals).

# Installation

## As unity module
This repository can be installed as unity module directly from git url. In this way new line should be added to `Packages/manifest.json`:
```
"com.leopotam.globals": "https://github.com/Leopotam/globals.git",
```
By default last released version will be used. If you need trunk / developing version then `develop` name of branch should be added after hash:
```
"com.leopotam.globals": "https://github.com/Leopotam/globals.git#develop",
```

## As source
If you can't / don't want to use unity modules, code can be downloaded as sources archive of required release from [Releases page](`https://github.com/Leopotam/globals/releases`).

# Classes

## Service
Service locator pattern support.

### Common usage
```csharp
class PlayerSession {
    public int Rank;
}

// init instance.
Service<PlayerSession>.Set (new PlayerSession ());
// ...
// request instance.
Service<PlayerSession>.Get ().Rank = 10;
// ...
// cleanup.
Service<PlayerSession>.Set (null);
```

### Usage with automatic creation of instance
```csharp
class PlayerSession {
    public int Rank;
}

// request instance. if not exists - will be created through default constructor.
Service<PlayerSession>.Get(true).Rank = 10;
// ...
// cleanup.
Service<PlayerSession>.Set (null);
```

# License
The software released under the terms of the [MIT license](./LICENSE.md). Enjoy.

# Donate
Its free opensource software, but you can buy me a coffee:

<a href="https://www.buymeacoffee.com/leopotam" target="_blank"><img src="https://www.buymeacoffee.com/assets/img/custom_images/yellow_img.png" alt="Buy Me A Coffee" style="height: auto !important;width: auto !important;" ></a>