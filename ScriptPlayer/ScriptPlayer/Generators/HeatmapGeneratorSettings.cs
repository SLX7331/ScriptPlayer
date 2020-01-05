﻿namespace ScriptPlayer.Generators
{
    public class HeatmapGeneratorSettings : FfmpegGeneratorSettings
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public bool AddShadow { get; set; }

        public HeatmapGeneratorSettings Duplicate()
        {
            return new HeatmapGeneratorSettings
            {
                AddShadow = AddShadow,
                Height = Height,
                SkipIfExists = SkipIfExists,
                Width = Width
            };
        }

        public HeatmapGeneratorSettings()
        {
            Width = 400;
            Height = 20;
            AddShadow = true;
            SkipIfExists = false;
        }

        public override bool IsIdenticalTo(FfmpegGeneratorSettings settings)
        {
            if (!(settings is HeatmapGeneratorSettings heatmapSettings))
                return false;

            if (!base.IsIdenticalTo(settings))
                return false;

            if (heatmapSettings.Width != Width) return false;
            if (heatmapSettings.Height != Height) return false;
            if (heatmapSettings.AddShadow != AddShadow) return false;

            return true;
        }
    }
}