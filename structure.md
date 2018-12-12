### Pugster Dashboard

Front end website.

##### Front page

- Configurable social media links
- Adjust layout if configured stream is live
- Adjust layout if any pugs are active

##### Moderator end
- Create and manage pugs
- Send announcements
- Generate team compositions based on selected user roles, ranking, or unweighted.


##### User end
- Choose preferred roles: tank, damage, support, flex, or spectate
- Choose preferred heroes: 3 preferred heroes per role (5 for damage, none for spectate)
- Subscribe to pug announcements
- View, join, and leave scheduled pugs
- Volunteer pov stream option
- Add battle tags for alt accounts


### Pugster Bridge

Forwards events from non-http connections into the Pugster signalr hubs.

##### Discord
- Various command-related chat events
- User joined server, give temp subscriber role while discord's integration loads
- Optionally lock user pug profile if they leave the configured discord server
- Quick view information on a pug or user
- Quick list upcoming pugs

##### Twitch
- Various command-related chat events
- Announce pug reminders


### Pugster Api

Rest api interface, signalr event hub, and various http stuff handling.

#### Webhooks
- Twitch's messed up websub system
- Twitter's stream api


### Pugster Core

Shared classes between all Pugster projects
- Database models
- Hub connections