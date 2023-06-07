﻿using System.ComponentModel;
using StabilityMatrix.Models.Api;
using StabilityMatrix.ViewModels;

namespace StabilityMatrix.DesignData;

[DesignOnly(true)]
public class MockCheckpointManagerViewModel : CheckpointManagerViewModel
{
    public MockCheckpointManagerViewModel() : base(null!)
    {
        CheckpointFolders = new()
        {
            new()
            {
                Title = "Stable Diffusion",
                CheckpointFiles = new()
                {
                    new()
                    {
                        Title = "Stable Diffusion v1.5",
                        FilePath = "v1-5-pruned-emaonly.safetensors",
                    },
                    new()
                    {
                        Title = "Scenery Mix",
                        FilePath = "scenery-mix.pt",
                    },
                    new()
                    {
                        Title = "Some Model",
                        FilePath = "exr-v3.safetensors",
                        ConnectedModel = new()
                        {
                            ModelName = "Example Realistic",
                            VersionName = "v3.0-Inpainting",
                            ModelDescription = "Example Description",
                            BaseModel = "SD 1.5",
                            FileMetadata = new()
                            {
                                Fp = CivitModelFpType.fp32,
                            }
                        }
                    },
                    new()
                    {
                        Title = "Painting e12",
                        FilePath = "painting-e12.pt",
                        ConnectedModel = new()
                        {
                            ModelName = "Long Name Model (Stuff)",
                            VersionName = "v42-Advanced-Hybrid",
                            ModelDescription = "Example Description",
                            BaseModel = "SD 2.0",
                            FileMetadata = new()
                            {
                                Fp = CivitModelFpType.fp16,
                            }
                        }
                    },
                }
            },
            new()
            {
                Title = "Lora",
                IsCurrentDragTarget = true,
                CheckpointFiles = new()
                {
                    new()
                    {
                        Title = "Detail Tweaker LoRA",
                        FilePath = "add_detail.safetensors",
                    },
                    new()
                    {
                        Title = "Armor Suit LoRa",
                        FilePath = "ArmorSuit_v1.safetensors",
                    },
                }
            }
        };
    }
}