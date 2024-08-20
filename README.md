
# Your-Lab.ch

[![Codacy Badge](https://api.codacy.com/project/badge/Grade/36d7c8c819c54290b38fe24b3ce3d949)](https://app.codacy.com/gh/Hutch79/Your-Lab?utm_source=github.com&utm_medium=referral&utm_content=Hutch79/Your-Lab&utm_campaign=Badge_Grade)

The goal of Your-Lab.ch is to (hopefully) lower the hurdle for beginners to get started with an IT project.
Anyone can obtain a free third-level domain (subdomain) that they can manage independently.

Currently, planned features will be either entirely free to use or have a free starting plan. Our goal is that the cost per feature should never exceed 5$ a year.

## Roadmap

Please keep in mind that these plans are more conceptual than concrete.   
They may change at any time and should not be considered a guarantee.  

- DynDNS
- Web Redirects (Subdomain redirecting to XYZ)
- URL-Shortener (With personal Your-Lab domain)
- Mail Redirect

## Environment Variables

To run this project, you will need to add the following environment variables to your .env file

| Environment Variable                     | Description                                          | Example        | Default |
|------------------------------------------|------------------------------------------------------|----------------|---------|
| `YOUR_LAB__DB__HOST`                     | Database host                                        | localhost      | -       |
| `YOUR_LAB__DB__PORT`                     | Database Port                                        | 3306           | 5432    |
| `YOUR_LAB__DB__DATABASE`                 | Database Name                                        | Your-Lab-DB    | -       |
| `YOUR_LAB__DB__USER`                     | Database User                                        | your-lab       | -       |
| `YOUR_LAB__DB__PASSWORD`                 | Database Password for the user                       | securePassword | -       |
| `YOUR_LAB__API_TOKEN__HETZNER_DNS`       | Hetzner API Token to manage DNS records              |                | -       |
| `YOUR_LAB__API_TOKEN__HETZNER_DNS`       | Cloudflare API Token to manage DNS records           |                | -       |
| `YOUR_LAB__API__TOKEN_LIMIT`             | Max amount of tokens                                 | 200            | 100     |
| `YOUR_LAB__API__TOKENS_PER_PERIOD`       | How many tokens to replenish per period              | 10             | 1       |
| `YOUR_LAB__API__REPLENISHMENT_PERIOD_MS` | Period length in milliseconds                        | 1000           | 600     |
| `YOUR_LAB__API__QUEUE_LIMIT`             | How many requests to queue if all tokens are used up | 10             | 0       |
