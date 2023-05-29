using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.HostAggregate.ValueObjects;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Itinerary;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Itinerary.Entities;
using Progetto_BDRC_SessioneEstiva_2022_2023.Domain.Itinerary.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Infrastructure.Persistence.Configurations
{
	public class ItineraryConfigurations : IEntityTypeConfiguration<Itinerary>
	{
		public void Configure(EntityTypeBuilder<Itinerary> builder)
		{
			ConfigureItineraryTable(builder);
			ConfigureItinerarySectionsTable(builder);
			ConfigureMenuIdsTable(builder);
			ConfigureItineraryReviewsIdsTable(builder);
		}

		private void ConfigureItineraryReviewsIdsTable(EntityTypeBuilder<Itinerary> builder)
		{
			builder.OwnsMany(m => m.ItineraryReviewIds, dib =>
			{
				dib.ToTable("ItineraryReviewIds");
				dib.WithOwner().HasForeignKey("ItineraryId");
				dib.HasKey("Id");
				dib.Property(d => d.Value)
					.HasColumnName("ItineraryReviewId")
					.ValueGeneratedNever();
			});

			builder.Metadata.FindNavigation(nameof(Itinerary.ItineraryReviewIds))!
				.SetPropertyAccessMode(PropertyAccessMode.Field);
		}

		private void ConfigureMenuIdsTable(EntityTypeBuilder<Itinerary> builder)
		{
			builder.OwnsMany(m => m.DinnerIds, dib =>
			{
				dib.ToTable("MenuDinnerIds");
				dib.WithOwner().HasForeignKey("ItineraryId");
				dib.HasKey("Id");
				dib.Property(d => d.Value)
					.HasColumnName("DinnerId")
					.ValueGeneratedNever();
			});

			builder.Metadata.FindNavigation(nameof(Itinerary.DinnerIds))!
				.SetPropertyAccessMode(PropertyAccessMode.Field);
		}

		private void ConfigureItinerarySectionsTable(EntityTypeBuilder<Itinerary> builder)
		{
			builder.OwnsMany(i => i.Sections, sb =>
			{
				sb.ToTable("ItinerarySections");

				sb.WithOwner().HasForeignKey("ItineraryId");

				sb.HasKey("Id", "ItineraryId");

				sb.Property(s => s.Id)
					.HasColumnName("ItinerarySectionId")
					.ValueGeneratedNever()
					.HasConversion(id => id.Value, value => ItinerarySectionId.Create(value));

				sb.Property(s => s.Name)
					.HasMaxLength(100);

				sb.Property(s => s.Description)
					.HasMaxLength(100);

				sb.OwnsMany(s => s.Items, ib =>
				{
					ib.ToTable("ItineraryItems");

					ib.WithOwner().HasForeignKey("ItinerarySectionId", "ItineraryId");

					ib.HasKey(nameof(ItineraryItem.Id), "ItinerarySectionId", "ItineraryId");

					ib.Property(i => i.Id)
						.HasColumnName("ItineraryItemId")
						.ValueGeneratedNever()
						.HasConversion(id => id.Value, value => ItineraryItemId.Create(value));

					ib.Property(i => i.Name)
					.HasMaxLength(100);

					ib.Property(i => i.Description)
						.HasMaxLength(100);
				});

				sb.Navigation(s => s.Items).Metadata.SetField("_items");
				sb.Navigation(s => s.Items).UsePropertyAccessMode(PropertyAccessMode.Field);

			});

			builder.Metadata.FindNavigation(nameof(Itinerary.Sections))!
				.SetPropertyAccessMode(PropertyAccessMode.Field);

		}

		private void ConfigureItineraryTable(EntityTypeBuilder<Itinerary> builder)
		{
			builder.ToTable("Itineraries");

			builder.HasKey(i => i.Id);

			builder.Property(i => i.Id)
				.ValueGeneratedNever()
				.HasConversion(id => id.Value, value => ItineraryId.Create(value));

			builder.Property(i => i.Name)
				.HasMaxLength(100);

			builder.Property(i => i.Description)
				.HasMaxLength(100);

			builder.OwnsOne(i => i.AverageRating);

			builder.Property(i => i.HostId)
				.HasConversion(id => id.Value, value => HostId.Create(value));
		}
	}
}
